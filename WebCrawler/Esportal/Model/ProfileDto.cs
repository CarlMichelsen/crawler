using System.Text.Json.Serialization;

namespace WebCrawler.Esportal.Model;

public class ProfileDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    // this value does not exsist in response (might not even want it)
    //public DateTime LastUpdated { get; set; }

    [JsonPropertyName("avatar_hash")]
    public string AvatarHash { get; set; } = string.Empty;

    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }

    [JsonPropertyName("display_medals")]
    public int DisplayMedals { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    //[JsonPropertyName("permissions")]
    //public List<null>? Permissions { get; set; } // not sure how to map this

    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    [JsonPropertyName("losses")]
    public int Losses { get; set; }

    [JsonPropertyName("elo")]
    public int Elo { get; set; }

    [JsonPropertyName("recent_kills")]
    public int RecentKills { get; set; }

    [JsonPropertyName("recent_deaths")]
    public int RecentDeaths { get; set; }

    [JsonPropertyName("recent_kdr_matches")]
    public int RecentKdrMatches { get; set; }

    [JsonPropertyName("recent_headshots")]
    public int RecentHeadshots { get; set; }

    [JsonPropertyName("recent_wins")]
    public int RecentWins { get; set; }

    [JsonPropertyName("recent_losses")]
    public int RecentLosses { get; set; }

    [JsonPropertyName("recent_matches")]
    public int RecentMatches { get; set; }

    [JsonPropertyName("recent_drops")]
    public int RecentDrops { get; set; }

    [JsonPropertyName("kills")]
    public int Kills { get; set; }

    [JsonPropertyName("deaths")]
    public int Deaths { get; set; }

    [JsonPropertyName("matches")]
    public int Matches { get; set; }

    [JsonPropertyName("drops")]
    public int Drops { get; set; }

    [JsonPropertyName("gathers_played")]
    public int GathersPlayed { get; set; }

    [JsonPropertyName("gather_drops")]
    public int GatherDrops { get; set; }

    [JsonPropertyName("favorite_weapon_id")]
    public int FavoriteWeaponId { get; set; }

    [JsonPropertyName("favorite_map_id")]
    public int FavoriteMapId { get; set; }

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("assists")]
    public int Assists { get; set; }

    [JsonPropertyName("mvps")]
    public int Mvps { get; set; }

    [JsonPropertyName("headshots")]
    public int Headshots { get; set; }

    [JsonPropertyName("damage_dealt")]
    public int DamageDealt { get; set; }

    [JsonPropertyName("rounds")]
    public int Rounds { get; set; }

    [JsonPropertyName("opening_duel_wins")]
    public int OpeningDuelWins { get; set; }

    [JsonPropertyName("clutches")]
    public int Clutches { get; set; }

    [JsonPropertyName("longest_winning_streak")]
    public int LongestWinningStreak { get; set; }

    [JsonPropertyName("longest_losing_streak")]
    public int LongestLosingStreak { get; set; }

    [JsonPropertyName("top_frags")]
    public int TopFrags { get; set; }

    [JsonPropertyName("overtime_wins")]
    public int OvertimeWins { get; set; }

    [JsonPropertyName("dominations")]
    public int Dominations { get; set; }

    [JsonPropertyName("scores_100")]
    public int Scores100 { get; set; }

    [JsonPropertyName("kd_over_3")]
    public int kdOver3 { get; set; }

    [JsonPropertyName("bomb_plants")]
    public int BombPlants { get; set; }

    [JsonPropertyName("bomb_defuses")]
    public int BombDefuses { get; set; }

    [JsonPropertyName("aces")]
    public int Aces { get; set; }

    [JsonPropertyName("one_hp_survivals")]
    public int OneHpSurvivals { get; set; } // håber det er bedre kvalitet end magnus

    [JsonPropertyName("eco_aces")]
    public int EcoAces { get; set; }

    [JsonPropertyName("eco_wins")]
    public int EcoWins { get; set; }

    [JsonPropertyName("frag_steals")]
    public int FragSteals { get; set; }

    [JsonPropertyName("awp_duel_wins")]
    public int AwpDuelWins { get; set; }

    [JsonPropertyName("banana_kills")]
    public int BananaKills { get; set; }

    [JsonPropertyName("mid_dust_kills")]
    public int MidDustKills { get; set; }

    //[JsonPropertyName("fastest_ace")]
    //public null FastestAce { get; set; } //(don't have a type for this)

    //[JsonPropertyName("fastest_opening_kill")]
    //public null FastestOpeningKill { get; set; } //(don't have a type for this)

    [JsonPropertyName("challenge_wins")]
    public int ChallengeWins { get; set; }

    [JsonPropertyName("opening_kills")]
    public int OpeningKills { get; set; }

    [JsonPropertyName("opening_deaths")]
    public int OpeningDeaths { get; set; }

    [JsonPropertyName("gathers_created")]
    public int GathersCreated { get; set; }

    [JsonPropertyName("games_as_legend")]
    public int GamesAsLegend { get; set; }
    
    [JsonPropertyName("premium_elapsed_time")]
    public ulong PremiumElapsedTime { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("online_status")]
    public int OnlineStatus { get; set; } //TODO: create enum for this

    [JsonPropertyName("thumbs_up")]
    public int ThumbsUp { get; set; }

    [JsonPropertyName("thumbs_down")]
    public int ThumbsDown { get; set; }

    [JsonPropertyName("inserted")]
    public int Inserted { get; set; } // TODO: find out what this means

    [JsonPropertyName("banned")]
    public bool Banned { get; set; }

    //[JsonPropertyName("ban")]
    //public null ban { get; set; } // TODO: make model for this

    //[JsonPropertyName("region_bans")]
    //public null RegionBans { get; set; } // TODO: make model for this

    [JsonPropertyName("region_id")]
    public int RegionId { get; set; }

    [JsonPropertyName("subregion_id")]
    public int SubregionId { get; set; }

    [JsonPropertyName("twitch_username")]
    public string TwitchUsername { get; set; } = string.Empty;

    [JsonPropertyName("friends")]
    public List<FriendDto> Friends { get; set; } = new List<FriendDto>();

    [JsonPropertyName("old_usernames")]
    public List<OldUsername> OldUsernames { get; set; } = new List<OldUsername>();

    [JsonPropertyName("medals")]
    public List<MedalDto> Medals { get; set; } = new List<MedalDto>();

    [JsonPropertyName("current_match")]
    public CurrentMatchDto? CurrentMatch { get; set; }

    [JsonPropertyName("current_gather_id")]
    public long? CurrentGatherId { get; set; }

    [JsonPropertyName("current_tournament_lobby")]
    public long? CurrentTournamentLobby { get; set; }

    //[JsonPropertyName("team")]
    //public null Team { get; set; }

    //[JsonPropertyName("nel_region_id")]
    //public null NelRegionId { get; set; }

    [JsonPropertyName("lemondogs_user_id")]
    public long? LemondogsUserId { get; set; }

    [JsonPropertyName("match_drops")]
    public List<MatchDropDto> MatchDrops { get; set; } = new List<MatchDropDto>();

    [JsonPropertyName("completed_missions")]
    public List<CompletedMissionDto> CompletedMissions { get; set; } = new List<CompletedMissionDto>();
    
    [JsonPropertyName("mission1_progress")]
    public int Mission1Progress { get; set; }

    [JsonPropertyName("mission2_progress")]
    public int Mission2Progress { get; set; }

    [JsonPropertyName("mission3_progress")]
    public int Mission3Progress { get; set; }

    [JsonPropertyName("extra_counter")]
    public ulong? ExtraCounter { get; set; }
}