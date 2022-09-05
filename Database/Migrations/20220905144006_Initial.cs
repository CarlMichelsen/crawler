using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FailedUnknownEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedUnknownEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileConnectionEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SteamId64 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileConnectionEntity", x => x.Id);
                });

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
                    ThumbsUp = table.Column<int>(type: "int", nullable: false),
                    ThumbsDown = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    DisplayMedals = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Flags = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    SubregionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banned = table.Column<bool>(type: "bit", nullable: false),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Flags = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ProfileConnectionsId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    StatsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RecentStatsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileEntity_ProfileConnectionEntity_ProfileConnectionsId",
                        column: x => x.ProfileConnectionsId,
                        principalTable: "ProfileConnectionEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfileEntity_RecentStatsEntity_RecentStatsId",
                        column: x => x.RecentStatsId,
                        principalTable: "RecentStatsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileEntity_StatsEntity_StatsId",
                        column: x => x.StatsId,
                        principalTable: "StatsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnknownEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnknownEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileEntityUserEntity",
                columns: table => new
                {
                    FriendsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IncompleteFriendsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileEntityUserEntity", x => new { x.FriendsId, x.IncompleteFriendsId });
                    table.ForeignKey(
                        name: "FK_ProfileEntityUserEntity_ProfileEntity_IncompleteFriendsId",
                        column: x => x.IncompleteFriendsId,
                        principalTable: "ProfileEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileEntityUserEntity_UserEntity_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsernameEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileEntityId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsernameEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsernameEntity_ProfileEntity_ProfileEntityId",
                        column: x => x.ProfileEntityId,
                        principalTable: "ProfileEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntity_ProfileConnectionsId",
                table: "ProfileEntity",
                column: "ProfileConnectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntity_RecentStatsId",
                table: "ProfileEntity",
                column: "RecentStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntity_StatsId",
                table: "ProfileEntity",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileEntityUserEntity_IncompleteFriendsId",
                table: "ProfileEntityUserEntity",
                column: "IncompleteFriendsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownEntity_UserId",
                table: "UnknownEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsernameEntity_ProfileEntityId",
                table: "UsernameEntity",
                column: "ProfileEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailedUnknownEntity");

            migrationBuilder.DropTable(
                name: "ProfileEntityUserEntity");

            migrationBuilder.DropTable(
                name: "UnknownEntity");

            migrationBuilder.DropTable(
                name: "UsernameEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "ProfileEntity");

            migrationBuilder.DropTable(
                name: "ProfileConnectionEntity");

            migrationBuilder.DropTable(
                name: "RecentStatsEntity");

            migrationBuilder.DropTable(
                name: "StatsEntity");
        }
    }
}
