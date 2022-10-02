using Database;
using Database.Entities;
using Database.Repositories;
using WebCrawler.Esportal.Services;

namespace WebCrawler.Esportal;

public class EsportalCrawler : ICrawler<UnknownEntity>
{
    private readonly EsportalProfileService _profileService;
    private readonly DataContext _context;

    public EsportalCrawler(EsportalProfileService profileService, DataContext context)
    {
        _profileService = profileService;
        _context = context;
    }

    public async Task<UnknownEntity?> Next()
    {
        var unknown = await UnknownRepository.GetNextUnknown(_context);
        if (unknown is not null) return unknown;

        var outdatedProfile = await ProfileRepository.GetNextOutDated(_context, TimeSpan.FromDays(14));
        var outdatedUser = await UserRepository.GetUserById(_context, outdatedProfile?.Id);
        if (outdatedUser is not null) return new UnknownEntity{ User = outdatedUser, Recorded = DateTime.Now };

        return null;
    }

    public async Task<bool> Act(ulong? userId)
    {
        if (userId is null) return false;
        var exsisting = await ProfileRepository.GetProfileById(_context, userId);

        try
        {
            if (exsisting is null)
            {
                return await HandleNew((ulong)userId);
            }
            else
            {
                return await HandleExsisting(exsisting);
            }
        }
        catch (HttpRequestException e)
        {
            // this is a transient error
            Console.WriteLine($"Assumed transient HttpRequestException: ${e.Message}");
            return false;
        }
        catch (Exception e)
        {
            // this is a fatal error
            Console.WriteLine($"Assumed fatal error: ${e.Message}");
            await HandleFatalError((ulong)userId, e.Message);
        }
        return true;
    }

    private async Task HandleFatalError(ulong userId, string errorMessage)
    {
        var unk = await UnknownRepository.GetUnknownByUserId(_context, userId);

        var addedFailedUnknownSuccessfully = await FailedUnknownRepository.AddFailedUnknown(
            _context,
            new FailedUnknownEntity{
                UserId = userId,
                ErrorMessage = errorMessage,
                Recorded = DateTime.Now
            }
        );
        if (!addedFailedUnknownSuccessfully) throw new Exception("Failed to addFailedUnknown.");

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
            profile.Recorded = DateTime.Now;
            Console.WriteLine(profile.ToString()+" --- NEW");
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
            updatedProfile.Recorded = DateTime.Now;
            Console.WriteLine(updatedProfile.ToString()+" --- UPDATED");
            return await ProfileRepository.UpdateProfileTransaction(_context, updatedProfile);
        }

        return false;
    }
}