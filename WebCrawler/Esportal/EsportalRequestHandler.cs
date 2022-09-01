using Database;
using Database.Entities;
using WebCrawler.Esportal.Model;
using WebCrawler.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Database.Repositories;
using System.Text;

namespace WebCrawler.Esportal;

public class EsportalRequestHandler : IRequestHandler<UnknownEntity, ProfileEntity>
{
    private DataContext _context;

    public EsportalRequestHandler()
    {
        _context = new DataContext();
    }

    public async Task<UnknownEntity?> GetNext()
    {
        if (_context.UnknownEntity is null) throw new InvalidOperationException("Invalid UnknownEntity DataContext.");
        var next = await _context.UnknownEntity.Where(unk => unk.Id != 0).Include(unk => unk.User).FirstAsync();
        if (next is null || next.User is null || next?.User.Id == 0) return null;
        return next;
    }

    public Uri ToUri(UnknownEntity input)
    {
        if (input.User.Id == 0) throw new InvalidDataException("Id can't be 0 when creating an esportal uri");
        return EsportalUri(ProfileRequestConfig.AllTrue(input.User.Id));
    }

    public HttpClient ClientFactory()
    {
        var client = new HttpClient();
        return client;
    }

    public async Task<ProfileEntity?> HandleNext(UnknownEntity? next)
    {
        if (_context.ProfileEntity is null) throw new InvalidOperationException("Invalid ProfileEntity DataContext.");
        if (next is null) return default(ProfileEntity);
        if (next.User is null) return default(ProfileEntity);
        if (next.User.Id == 0) return default(ProfileEntity);

        if (await _context.ProfileEntity.AnyAsync(profile => profile.Id == next.User.Id))
        {
            Console.WriteLine($"Tried to crawl exsisting profile {next.User.Username}");
            return default(ProfileEntity);
        }

        // get httpClient
        var client = ClientFactory();

        // send http request
        var uri = ToUri(next);
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await client.SendAsync(request);

        // read response in stream
        return await HandleRequestResponse(response, next);
    }

    public async Task<ProfileEntity?> HandleRequestResponse(HttpResponseMessage response, UnknownEntity current)
    {
        var content = await response.Content.ReadAsStringAsync();
        var success = TrySerializeAndMapProfileDto(content, out var result);
        if (!success) Console.WriteLine($"{current.User.Username} failed serialization <{current.User.Id}>");
        return result;
    }

    private string StringFromUnknown(UnknownEntity unk)
    {
        StringBuilder sb = new(unk.User.Username);
        //sb.Append(new string(' ', Math.Clamp(25-unk.User.Username.Length, 5, 25)));
        return sb.ToString();
    }

    public async Task<bool> FinalizeNext(UnknownEntity? next, ProfileEntity? result, string? errorMessage)
    {
        if (next is not null && result is not null)
        {
            var success = await ProfileRepository.AddProfile(result);
            if (success)
            {
                await UnknownRepository.RemoveUnknown(next.Id);
                Console.WriteLine(StringFromUnknown(next));
            }
            return success;
        }
        else
        {
            if (next is null) return false;
            var success = await UnknownRepository.RemoveUnknown(next.Id);
            if (success)
            {
                var failed = new FailedUnknownEntity()
                {
                    UserId = next.User.Id,
                    ErrorMessage = errorMessage,
                    Recorded = DateTime.Now
                };
                await FailedUnknownRepository.AddFailedUnknown(failed);
            }
            return false;
        }
    }

    private UnknownEntity UserEntityToUnknownEntity(UserEntity input)
    {
        return new UnknownEntity()
        {
            User = input,
            Recorded = DateTime.Now
        };
    }

    private bool TrySerializeAndMapProfileDto(string input, out ProfileEntity? profile)
    {
        ProfileEntity? temp = null;
        try
        {
            if (string.IsNullOrWhiteSpace(input)) throw new InvalidDataException("Attempted to serialize and map a null or whitespace string");
            var dto = JsonSerializer.Deserialize<ProfileDto>(input);
            temp = EsportalMapper.Mapper.Map<ProfileEntity>(dto);
            temp.Stats = EsportalMapper.Mapper.Map<StatsEntity>(dto);
            temp.RecentStats = EsportalMapper.Mapper.Map<RecentStatsEntity>(dto);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (temp is not null) 
            {
                profile = temp;
            }
            else 
            {
                profile = null;
            }
        }

        if (temp is not null) return true;
        return false;
    }

    private Uri EsportalUri(ProfileRequestConfig config) {
        var protocol = "https";
        var host = "esportal.com";
        var path = "api/user_profile/get";
        return new Uri($"{protocol}://{host}/{path}?{config.Query()}");
    }
}