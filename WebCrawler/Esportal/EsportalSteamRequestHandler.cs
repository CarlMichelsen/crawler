using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebCrawler.Esportal;

public class EsportalSteamRequestHandler : IRequestHandler<ProfileEntity, ProfileConnectionEntity>
{
    private DataContext _context;
    
    public EsportalSteamRequestHandler()
    {
        _context = new DataContext();
    }

    public async Task<ProfileEntity?> GetNext()
    {
        if (_context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity is null");
        return await _context.ProfileEntity.FirstOrDefaultAsync();
    }

    public async Task<ProfileConnectionEntity?> HandleNext(ProfileEntity? next)
    {
        if (next is null) throw new NullReferenceException("No ProfileEntity for EsportalSteamRequestHandler.");
        
        HttpResponseMessage? response = null;
        try
        {
            // get httpClient
            var client = ClientFactory();

            var uri = ToUri(next);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var htmlResponse = await client.SendAsync(request);
            var str = await htmlResponse.Content.ReadAsStringAsync();

            var javascriptLocation = str.IndexOf("<script src=\"assets/main");
            var scriptTagEstimate = str.Substring(javascriptLocation, 150);

            var firstQuoteIndex = scriptTagEstimate.IndexOf("\"")+1;
            var scriptEndIndex = scriptTagEstimate.IndexOf(".js\"")-10;
            var scriptUri = scriptTagEstimate.Substring(firstQuoteIndex, scriptEndIndex);
            var fullScriptUri = new Uri($"https://esportal.com/{scriptUri}");
            //https://esportal.com/assets/main.cbb8565f5b6a095e9f5f.js.map

            request = new HttpRequestMessage(HttpMethod.Get, fullScriptUri);
            response = await client.SendAsync(request);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            throw new HttpRequestException("Request failed");
        }

        return await HandleRequestResponse(response, next);
    }

    public async Task<ProfileConnectionEntity?> HandleRequestResponse(HttpResponseMessage response, ProfileEntity current)
    {
        var javascriptString = await response.Content.ReadAsStringAsync();

        System.Console.WriteLine(javascriptString);

        throw new NotImplementedException("AAA");
    }

    public Task<bool> FinalizeNext(ProfileEntity? next, ProfileConnectionEntity? result, string? errorMessage)
    {
        throw new NotImplementedException();
    }

    public Uri ToUri(ProfileEntity input)
    {
        return new Uri($"https://esportal.com/da/profile/{input.Username}");
    }

    public HttpClient ClientFactory()
    {
        var client = new HttpClient();
        return client;
    }
}