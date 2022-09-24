namespace Api.Dto;

public class CrawlerStatusDto
{
    public string CrawlerName { get; set; } = string.Empty;
    public int ProfileAmount { get; set; }
    public int RemainingUnknowns { get; set; }
    public int FailedUnknowns { get; set; }
    public int SteamIdCount { get; set; }

    public override string ToString()
    {
        var str = $"{CrawlerName}: {ProfileAmount}/{RemainingUnknowns} [{FailedUnknowns}] <{SteamIdCount}>";
        return str;
    }
}