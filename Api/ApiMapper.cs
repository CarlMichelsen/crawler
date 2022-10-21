using AutoMapper;
using Database.Entities;
using Api.Dto;
using Api.Dto.Query;
using Services.Faceit.Model;
using Services.Steam.Model;

namespace Api;

public static class ApiMapper
{
    private static IMapper? _mapper;

    public static IMapper Mapper
    {
        get
        {
            if (_mapper is null) _mapper = CreateMapperConfiguration().CreateMapper();
            return _mapper;
        }
    }

    private static MapperConfiguration CreateMapperConfiguration()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProfileEntity, ProfileDto>();
            cfg.CreateMap<StatsEntity, StatsDto>();
            cfg.CreateMap<RecentStatsEntity, RecentStatsDto>();

            cfg.CreateMap<UsernameEntity, string>()
                .ConvertUsing(src => src.Username);

            cfg.CreateMap<UserEntity, string>()
                .ConvertUsing(src => src.Username);
        });
    }

    public static SteamAccount? MapSteamResponse(SteamResponse? src)
    {
        if (src is null) return default;
        var summary = src.Response.Players.FirstOrDefault();
        if (summary is null) return default;
        var acc = new SteamAccount
        {
            Username = summary.Personaname,
            RealName = summary.RealName,
            Avatar = summary.Avatar,
            AvatarMedium = summary.AvatarMedium,
            AvatarFull = summary.AvatarFull,
            AvatarHash = summary.AvatarHash,
            Country = summary.LocCountryCode,
            TimeCreated = summary.TimeCreated
        };
        return acc;
    }

    public static FaceitAccount? MapFaceitResponse(FaceitPlayerResponse? src)
    {
        if (src is null) return default;
        var acc = new FaceitAccount
        {
            FaceitId = src.PlayerId.ToString(),
            FaceitUsername = src.Nickname,
            Avatar = src.Avatar,
            CoverImage = src.CoverImage
        };

        var success = src.Games.TryGetValue("csgo", out FaceitGame? game);
        if (!success || game is null) return default;

        acc.SkillLevel = game.SkillLevel;
        acc.Elo = game.FaceitElo;
        acc.Region = game.Region;
        acc.Country = src.Country;
        acc.Language = src.Settings.Language;
        acc.FaceitUrl = src.FaceitUrl.Replace("{lang}", "en");
        acc.Friends = src.FriendsIds.Count;

        return acc;
    }
}