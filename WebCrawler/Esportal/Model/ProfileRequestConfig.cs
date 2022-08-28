namespace WebCrawler.Esportal.Model;

public class ProfileRequestConfig
{
    public bool Friends { get; set;} = false;
    public bool UsernameHistory { get; set;} = false;
    public bool Medals { get; set;} = false;
    public bool Levels { get; set;} = false;
    public bool CurrentMatch { get; set;} = false;
    public bool Bans { get; set;} = false;
    public bool Team { get; set;} = false;
    public bool Twitch { get; set;} = false;
    public bool Lemondogs { get; set;} = false;
    public bool Rank { get; set;} = false;
    public bool MatchDrop { get; set;} = false;
    public long Underscore { get; set; }
    public long Id { get; set; }

    public ProfileRequestConfig(long id)
    {
        Underscore = 1661724976679;
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

    public static ProfileRequestConfig AllTrue(long id) {
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

//https://esportal.com/api/user_profile/get?_=1661724826904&id=1661724145912&friends=1&username_history=1&medals=1&levels=1&current_match=1&twitch=1&team=1&lemondogs=1&rank=1&match_drop=1
//https://esportal.com/api/user_profile/get?_=1661724826904&id=122699457&current_match=1&rank=1