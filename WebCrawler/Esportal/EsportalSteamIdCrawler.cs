using System.Text.Json;
using Database;
using Database.Entities;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Esportal.Model;

namespace WebCrawler.Esportal;

public class EsportalSteamIdCrawler : ICrawler<ProfileEntity>
{
    private DataContext _context;
    
    public EsportalSteamIdCrawler()
    {
        _context = new DataContext();
    }

    public async Task<ProfileEntity?> Next()
    {
        if (_context.ProfileEntity is null) throw new InvalidOperationException("Invalid ProfileEntity DataContext.");
        Console.WriteLine("Attempting to get next SteamIdProfileEntity");
        var next = await EsportalSteamIdRepository.GetNextSteamIdCandidate(_context);

        Console.WriteLine($"Found {next?.Username ?? string.Empty}");
        return next;
    }

    private HttpClient HttpClientFactory()
    {
        return new HttpClient();
    }

    private async Task<string> RequestSteamId(Uri uri)
    {
        try
        {
            var req = new HttpRequestMessage();
            req.RequestUri = uri;
            req.Method = HttpMethod.Get;

            Console.WriteLine(req.RequestUri);

            var client = HttpClientFactory();
            var res = await client.SendAsync(req);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsStringAsync();
            } else {
                throw new Exception("request failed");
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private bool TrySerializeSteamIdDto(string input, out SteamIdDto? profile)
    {
        SteamIdDto? temp = null;
        try
        {
            if (string.IsNullOrWhiteSpace(input)) throw new InvalidDataException("Attempted to serialize a null or whitespace string");
            temp = JsonSerializer.Deserialize<SteamIdDto>(input);
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

    public async Task<bool> Act(ProfileEntity? input)
    {
        if (input is null)
        {
            return false;
        }
        if (_context.ProfileConnectionEntity is null) throw new InvalidOperationException("Invalid ProfileConnectionEntity DataContext.");
        var steamIdServiceUrl = Environment.GetEnvironmentVariable("STEAMID_SERVICE_URL") ?? "http://157.245.20.228:8080";
        var requestUri = new Uri($"{steamIdServiceUrl}/esportal-steamid/{input.Username}");
        var rawResponse = await RequestSteamId(requestUri);
        var successfulSerialization = TrySerializeSteamIdDto(rawResponse, out SteamIdDto? responseDto);
        if (!successfulSerialization) return false;
        if (responseDto?.Success == true && responseDto?.SteamId is not null)
        {
            return await EsportalSteamIdRepository.UpsertSteamId(input.Id, responseDto.SteamId);
        }
        return false;
    }
}