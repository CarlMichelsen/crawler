using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class CurrentMatchDto
{
    [JsonPropertyName("id")]
    public ulong? Id { get; set; }

    [JsonPropertyName("team")]
    public int Team { get; set; }

    [JsonPropertyName("team1_score")]
    public int Team1Score { get; set; }

    [JsonPropertyName("team2_score")]
    public int Team2Score { get; set; }

    [JsonPropertyName("gather_id")]
    public long? GatherId { get; set; }

    [JsonPropertyName("tournament_lobby")]
    public long? TournamentLobby { get; set; }
}