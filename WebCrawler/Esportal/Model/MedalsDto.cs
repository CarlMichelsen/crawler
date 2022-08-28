using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class MedalsDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("objective")]
    public long? Objective { get; set; }

    [JsonPropertyName("amount")]
    public int? Amount { get; set; }
    
    [JsonPropertyName("user_id")]
    public long? UserId { get; set; }

    [JsonPropertyName("match_id")]
    public long? MatchId { get; set; }

    [JsonPropertyName("inserted")]
    public long? Inserted { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("unique")]
    public bool Unique { get; set; }

    [JsonPropertyName("record")]
    public bool Record { get; set; }

    //[JsonPropertyName("prize")]
    //public null Prize { get; set; }

    //[JsonPropertyName("next_medal_amount")]
    //public null NextMedalAmount { get; set; }

    [JsonPropertyName("level")]
    public int? NextMedalAmount { get; set; }

    [JsonPropertyName("holder")]
    public long? Holder { get; set; }
}