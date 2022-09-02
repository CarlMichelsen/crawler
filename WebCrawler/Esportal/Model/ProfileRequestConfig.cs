namespace WebCrawler.Esportal.Model;

public class ProfileRequestConfig
{
    public bool Friends { get; set; } = false;
    public bool UsernameHistory { get; set; } = false;
    public bool Medals { get; set; } = false;
    public bool Levels { get; set; } = false;
    public bool CurrentMatch { get; set; } = false;
    public bool Bans { get; set; } = false;
    public bool Team { get; set; } = false;
    public bool Twitch { get; set; } = false;
    public bool Lemondogs { get; set; } = false;
    public bool Rank { get; set; } = false;
    public bool MatchDrop { get; set; } = false;
    public ulong Underscore { get; }
    public ulong Id { get; }

    private ProfileRequestConfig(ulong id)
    {
        Underscore = 1662125521534;
        Id = id;
    }
    
    public void SetAllTrue()
    {
        Friends = true;
        UsernameHistory = true;
        Medals = true;
        Levels = true;
        CurrentMatch = true;
        Bans = true;
        Team = true;
        Twitch = true;
        Lemondogs = true;
        Rank = true;
        MatchDrop = true;
    }

    public static ProfileRequestConfig AllTrue(ulong id) {
        var conf = new ProfileRequestConfig(id);
        conf.SetAllTrue();
        return conf;
    }

    public string Query()
    {
        List<string> queryElements = new();
        queryElements.Add($"_={Underscore}");
        queryElements.Add($"id={Id}");
        if (Friends) queryElements.Add("friends=1");
        if (UsernameHistory) queryElements.Add("username_history=1");
        if (Medals) queryElements.Add("medals=1");
        if (Levels) queryElements.Add("levels=1");
        if (CurrentMatch) queryElements.Add("current_match=1");
        if (Twitch) queryElements.Add("twitch=1");
        if (Team) queryElements.Add("team=1");
        if (Lemondogs) queryElements.Add("lemondogs=1");
        if (Rank) queryElements.Add("rank=1");
        if (MatchDrop) queryElements.Add("match_drop=1");
        return string.Join("&", queryElements);
    }
}