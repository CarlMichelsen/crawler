using Database;
using Database.Entities;
using Database.Repositories;
using WebCrawler.Esportal.Services;
using Microsoft.Extensions.Logging;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler<UnknownEntity>
{
    private readonly ILogger<EsportalCrawler> _logger;
    private readonly IDatabaseConfiguration _databaseConfiguration;
    private readonly EsportalProfileService _profileService;
    private readonly DataContext _context;

    public EsportalCrawler(ILogger<EsportalCrawler> logger, IDatabaseConfiguration databaseConfiguration, EsportalProfileService profileService)
    {
        _logger = logger;
        _databaseConfiguration = databaseConfiguration;
        _profileService = profileService;
        _context = new DataContext(_databaseConfiguration); // make sure backgroundservices get their own Datacontext
    }

    public async Task<UnknownEntity?> Next()
    {
        var unknown = await UnknownRepository.GetNextUnknown(_context);
        if (unknown is not null)
        {
            _logger.LogInformation("Found new account {username}", unknown.User.Username);
            return unknown;
        }

        var outdatedProfile = await ProfileRepository.GetNextOutDated(_context, TimeSpan.FromDays(14));
        var outdatedUser = await UserRepository.GetUserById(_context, outdatedProfile?.Id);
        if (outdatedUser is not null)
        {
            _logger.LogInformation("Updating exsisting user {username}", outdatedUser.Username);
            return new UnknownEntity{ User = outdatedUser, Recorded = DateTime.Now };
        }

        return null;
    }

    public async Task<bool> Act(ulong? userId)
    {
        if (userId is null) return false;
        var exsisting = await ProfileRepository.GetProfileById(_context, userId);
        if (exsisting != null) _logger.LogInformation("Found exsisting: {exsisting}", exsisting);

        try
        {
            if (exsisting == null)
            {
                var handled = await HandleNew((ulong)userId);
                if (handled) return await HandleComplete((ulong)userId);
            }
            else
            {
                var handled = await HandleExsisting(exsisting);
                if (handled) return await HandleComplete((ulong)userId);
                //throw new NotImplementedException("Can't handle exsisting yet");
            }
        }
        catch (HttpRequestException e)
        {
            // this is a transient error
            if (e?.StatusCode != null && (int)e.StatusCode >= 500)
            {
                _logger.LogWarning("Assumed transient HttpRequestException: {message}", e.Message);
                return false;
            }
            else
            {
                // this is a fatal error
                _logger.LogCritical("Assumed fatal error: {message}", e?.Message ?? string.Empty);
                await HandleFatalError((ulong)userId, e?.Message ?? string.Empty);
            }
        }
        catch (Exception e)
        {
            // this is a fatal error
            _logger.LogCritical("Assumed fatal error: {message}", e.Message);
            await HandleFatalError((ulong)userId, e.Message);
        }
        return false;
    }

    private async Task HandleFatalError(ulong userId, string errorMessage)
    {
        var unk = await UnknownRepository.GetUnknownByUserId(_context, userId);
        var profile = await ProfileRepository.GetProfileById(_context, userId);

        if (profile is null)
        {
            var addedFailedUnknownSuccessfully = await FailedUnknownRepository.AddFailedUnknown(
                _context,
                new FailedUnknownEntity{
                    UserId = userId,
                    ErrorMessage = errorMessage,
                    Recorded = DateTime.Now
                }
            );
            if (!addedFailedUnknownSuccessfully) throw new Exception("Failed to addFailedUnknown.");
        }
        else
        {
            var success = await ProfileRepository.SetRecordedDate(_context, profile.Id, DateTime.Now);
            if (!success) throw new Exception($"Failed to reset Recorded date on failed refresh of a ProfileEntity <{profile.Id}>");
        }
        

        var unknownId = unk?.Id;
        if (unknownId is not null)
        {
            var removedUnknownSuccessfully = await UnknownRepository.RemoveUnknown(
                _context,
                (ulong)unknownId
            );
            if (!removedUnknownSuccessfully) throw new Exception("Failed to remove Unknown.");
        }
    }

    private async Task<bool> HandleNew(ulong userId)
    {
        var profileJsonString = await _profileService.RequestProfileEntity(userId);
        var successfulParse = EsportalProfileService.TrySerializeAndMapProfileDto(profileJsonString, out ProfileEntity? profile);

        if (successfulParse && profile is not null)
        {
            return await ProfileRepository.AddProfileTransaction(_context, profile);
        }

        return false;
    }

    private async Task<bool> HandleExsisting(ProfileEntity profile)
    {
        var profileJsonString = await _profileService.RequestProfileEntity(profile.Id);
        var successfulParse = EsportalProfileService.TrySerializeAndMapProfileDto(profileJsonString, out ProfileEntity? updatedProfile);

        if (successfulParse && updatedProfile is not null)
        {
            return await ProfileRepository.UpdateProfileTransaction(_context, updatedProfile);;
        }

        return false;
    }

    private async Task<bool> HandleComplete(ulong userId)
    {
        var unk = await UnknownRepository.GetUnknownByUserId(_context, userId);
        if (unk is not null)
        {
            var success = await UnknownRepository.RemoveUnknown(_context, (ulong)unk.Id);;
            if (success)
            {
                var profile = await ProfileRepository.GetProfileById(_context, userId);
                _logger.LogInformation("Completed cleanup after fetching {username}", profile?.Username);
            }
            else
            {
                _logger.LogCritical("Failed to cleanup after fetching userid <{userId}>", userId);
            }
            return success;
        }
        return true; // if there is no unknown to remove (in case it was an update and not a discovery) return true
    }
}