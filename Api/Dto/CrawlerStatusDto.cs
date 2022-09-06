namespace Api.Dto;

public class CrawlerStatusDto
{
    public string CrawlerName { get; set; } = string.Empty;
    public int ProfileAmount { get; set; }
    public int RemainingUnknowns { get; set; }
    public int FailedUnknowns { get; set; }
    public string Status { get; set; } = string.Empty;
    public double SecondsRunning { get; set; }
}