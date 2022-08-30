using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecentStatsEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecentKills = table.Column<int>(type: "int", nullable: false),
                    RecentDeaths = table.Column<int>(type: "int", nullable: false),
                    RecentHeadshots = table.Column<int>(type: "int", nullable: false),
                    RecentWins = table.Column<int>(type: "int", nullable: false),
                    RecentLosses = table.Column<int>(type: "int", nullable: false),
                    RecentMatches = table.Column<int>(type: "int", nullable: false),
                    RecentDrops = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentStatsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatsEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Headshots = table.Column<int>(type: "int", nullable: false),
                    Matches = table.Column<int>(type: "int", nullable: false),
                    Drops = table.Column<int>(type: "int", nullable: false),
                    Mvps = table.Column<int>(type: "int", nullable: false),
                    DamageDealt = table.Column<int>(type: "int", nullable: false),
                    Rounds = table.Column<int>(type: "int", nullable: false),
                    OpeningDuelWins = table.Column<int>(type: "int", nullable: false),
                    OpeningKills = table.Column<int>(type: "int", nullable: false),
                    OpeningDeaths = table.Column<int>(type: "int", nullable: false),
                    Clutches = table.Column<int>(type: "int", nullable: false),
                    LongestWinningStreak = table.Column<int>(type: "int", nullable: false),
                    LongestLosingStreak = table.Column<int>(type: "int", nullable: false),
                    TopFrags = table.Column<int>(type: "int", nullable: false),
                    OvertimeWins = table.Column<int>(type: "int", nullable: false),
                    Dominations = table.Column<int>(type: "int", nullable: false),
                    Scores100 = table.Column<int>(type: "int", nullable: false),
                    kdOver3 = table.Column<int>(type: "int", nullable: false),
                    BombPlants = table.Column<int>(type: "int", nullable: false),
                    BombDefuses = table.Column<int>(type: "int", nullable: false),
                    Aces = table.Column<int>(type: "int", nullable: false),
                    OneHpSurvivals = table.Column<int>(type: "int", nullable: false),
                    EcoAces = table.Column<int>(type: "int", nullable: false),
                    EcoWins = table.Column<int>(type: "int", nullable: false),
                    FragSteals = table.Column<int>(type: "int", nullable: false),
                    AwpDuelWins = table.Column<int>(type: "int", nullable: false),
                    BananaKills = table.Column<int>(type: "int", nullable: false),
                    MidDustKills = table.Column<int>(type: "int", nullable: false),
                    GamesAsLegend = table.Column<int>(type: "int", nullable: false),
                    PremiumElapsedTime = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ThumbsUp = table.Column<int>(type: "int", nullable: false),
                    ThumbsDown = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    GathersPlayed = table.Column<int>(type: "int", nullable: false),
                    GathersCreated = table.Column<int>(type: "int", nullable: false),
                    GatherDrops = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Flags = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StatsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RecentStatsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_RecentStatsEntity_RecentStatsId",
                        column: x => x.RecentStatsId,
                        principalTable: "RecentStatsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_StatsEntity_StatsId",
                        column: x => x.StatsId,
                        principalTable: "StatsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    DisplayMedals = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Flags = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    SubregionId = table.Column<int>(type: "int", nullable: false),
                    ProfileEntityId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntity_Profiles_ProfileEntityId",
                        column: x => x.ProfileEntityId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsernameEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileEntityId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsernameEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsernameEntity_Profiles_ProfileEntityId",
                        column: x => x.ProfileEntityId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    MatchId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ProfileEntityId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchEntity_Profiles_ProfileEntityId",
                        column: x => x.ProfileEntityId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchEntity_ProfileEntityId",
                table: "MatchEntity",
                column: "ProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEntity_UserId",
                table: "MatchEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_RecentStatsId",
                table: "Profiles",
                column: "RecentStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_StatsId",
                table: "Profiles",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_ProfileEntityId",
                table: "UserEntity",
                column: "ProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UsernameEntity_ProfileEntityId",
                table: "UsernameEntity",
                column: "ProfileEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchEntity");

            migrationBuilder.DropTable(
                name: "UsernameEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "RecentStatsEntity");

            migrationBuilder.DropTable(
                name: "StatsEntity");
        }
    }
}
