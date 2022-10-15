using System.Text.Json;
using Database;
using Database.Entities;
using Database.Repositories;
using Microsoft.Extensions.Logging;
using WebCrawler.Esportal.Model;

namespace WebCrawler.Esportal;

public class EsportalSteamIdCrawler : ICrawler<ProfileEntity>
{
    private readonly IDatabaseConfiguration _databaseConfiguration;
    private readonly ILogger<EsportalSteamIdCrawler> _logger;
    private readonly DataContext _context;
    private readonly ISteamIdConfiguration _config;
    
    public EsportalSteamIdCrawler(ILogger<EsportalSteamIdCrawler> logger, IDatabaseConfiguration databaseConfiguration, ISteamIdConfiguration config)
    {
        _logger = logger;
        _databaseConfiguration = databaseConfiguration;
        _context = new DataContext(_databaseConfiguration); // make sure backgroundservices get their own Datacontext
        _config = config;
    }

    public async Task<ProfileEntity?> Next()
    {
        if (_context.ProfileEntity is null) throw new InvalidOperationException("Invalid ProfileEntity DataContext.");
        var next = await EsportalSteamIdRepository.GetNextSteamIdCandidate(_context);
        _logger.LogInformation("Attempting to fetch steamid for {username}", next?.Username ?? string.Empty);
        return next;
    }

    public async Task<bool> Act(ulong? userId)
    {
        if (userId is null) return false;
        var input = await ProfileRepository.GetProfileById(_context, userId);
        if (input is null) return false;
        if (_context.ProfileConnectionEntity is null) throw new InvalidOperationException("Invalid ProfileConnectionEntity DataContext.");
        var requestUri = new Uri($"{_config.EsportalSteamIdUrl}/{input.Username}");
        var rawResponse = await RequestSteamId(requestUri);
        var successfulSerialization = TrySerializeSteamIdDto(rawResponse, out SteamIdDto? responseDto);
        if (!successfulSerialization) return false;
        if (responseDto?.Success == true && responseDto?.SteamId is not null)
        {
            var success = await EsportalSteamIdRepository.UpsertSteamId(_context, input.Id, responseDto.SteamId);
            var actionString = success  ? "Saved" : "Failed to save";
            _logger.LogInformation("{actionString} {SteamId} as steamid for {Username}", actionString, responseDto.SteamId, input.Username);
            return success;
        }
        else if (responseDto?.TransientError == false)
        {
            var success = await EsportalSteamIdRepository.UpsertSteamId(_context, input.Id, null);
            var actionString = success  ? "Saved" : "Failed to save";
            _logger.LogInformation("{actionString} {SteamId} as steamid for {Username}", actionString, responseDto.SteamId, input.Username);
            return success;
        }
        return false;
    }

    private static HttpClient HttpClientFactory()
    {
        return new HttpClient();
    }

    private async Task<string> RequestSteamId(Uri uri)
    {
        try
        {
            var req = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            _logger.LogInformation("Fetching SteamId64 from {uri}", req.RequestUri);

            var client = HttpClientFactory();
            var res = await client.SendAsync(req);
            if (res.IsSuccessStatusCode)
            {
                var str = await res.Content.ReadAsStringAsync();
                return str;
            } else {
                throw new Exception("request failed");
            }
        }
        catch (Exception)
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
        catch (Exception e)
        {
            _logger.LogError("Exception \"{message}\"", e.Message);
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
}