using System.Text.Json;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Esportal.Model;
using WebCrawler.Mappers;

namespace WebCrawler.Esportal;

public class EsportalRequestHandler : BaseRequestHandler<UserEntity, ProfileEntity>
{
    private DataContext _context;

    public EsportalRequestHandler()
    {
        _context = new DataContext();
    }

    public override async Task<UserEntity?> GetNext()
    {
        if (_context.Unknowns is null) throw new InvalidOperationException("Invalid unknowns DataContext.");
        return await _context.Unknowns.FirstOrDefaultAsync();
    }

    public override Uri ToUri(UserEntity input)
    {
        return EsportalUri(ProfileRequestConfig.AllTrue(input.Id));
    }

    public override async Task<ProfileEntity?> HandleRequestResponse(HttpResponseMessage response, UserEntity current)
    {
        var content = await response.Content.ReadAsStringAsync();
        var success = TrySerializeAndMapProfileDto(content, out var result);
        if (!success) Console.WriteLine($"{current.Username} failed serialization <{current.Id}>");
        return result;
    }

    public override async Task<bool> FinalizeNext(UserEntity? next, ProfileEntity? result)
    {
        if (_context.Profiles is null) throw new InvalidOperationException("Invalid profiles DataContext.");
        if (_context.Unknowns is null) throw new InvalidOperationException("Invalid unknowns DataContext.");

        if (next is not null && result is not null)
        {
            /*var newUnknowns = new List<UserEntity>();
            foreach (var friend in result.Friends)
            {
                if (friend is null) continue;
                var existsAlready = await _context.Unknowns.AnyAsync((u) => u.Id == friend.Id);
                if (!existsAlready) existsAlready = await _context.Profiles.AnyAsync((p) => p.Id == friend.Id);
                if (existsAlready || newUnknowns.Any(f => f.Id == friend.Id)) continue;
                newUnknowns.Add(friend);
            }

            _context.Unknowns.AddRange(newUnknowns);
            _context.Unknowns.Remove(next);*/
            _context.Profiles.Add(result);
            await _context.SaveChangesAsync();
            return true;
        } //TODO: add else statement for cleanup
        return false;
    }

    private bool TrySerializeAndMapProfileDto(string input, out ProfileEntity? profile)
    {
        ProfileEntity? temp = null;
        try
        {
            var dto = JsonSerializer.Deserialize<ProfileDto>(input);
            temp = EsportalMapper.Mapper.Map<ProfileEntity>(dto);
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