using System.Text.Json;
using Database.Entities;
using WebCrawler.Esportal.Model;
using WebCrawler.Mappers;

namespace WebCrawler.Esportal.Services;

public class EsportalProfileService
{
    public readonly HttpClient _httpClient;

    public EsportalProfileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RequestProfileEntity(ulong? userId)
    {
        if (userId is null) throw new HttpRequestException("userId is null.");
        var uri = EsportalUri(userId);
        if (uri is null) throw new HttpRequestException("Could not create uri.");
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public static bool TrySerializeAndMapProfileDto(string input, out ProfileEntity? profile)
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

    private static Uri? EsportalUri(ulong? userId)
    {
        if (userId is null) return null;
        return EsportalUriFactory(ProfileRequestConfig.AllTrue((ulong)userId));
    }

    private static Uri EsportalUriFactory(ProfileRequestConfig config) {
        var protocol = "https";
        var host = "esportal.com";
        var path = "api/user_profile/get";
        return new Uri($"{protocol}://{host}/{path}?{config.Query()}");
    }
}