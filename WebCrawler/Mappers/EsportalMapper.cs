using AutoMapper;
using Database.Entities;
using WebCrawler.Esportal.Model;

namespace WebCrawler.Mappers;

public static class EsportalMapper
{
    private static IMapper? _mapper;

    public static IMapper Mapper
    {
        get {
            if (_mapper is null) _mapper = CreateMapperConfiguration().CreateMapper();
            return _mapper;
        }
    }

    private static MapperConfiguration CreateMapperConfiguration()
    {
        return new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<ProfileDto, ProfileEntity>();
            cfg.CreateMap<ProfileDto, StatsEntity>();
            cfg.CreateMap<ProfileDto, RecentStatsEntity>();

            cfg.CreateMap<MatchDropDto, MatchEntity>();
            cfg.CreateMap<CurrentMatchDto, MatchEntity>();

            cfg.CreateMap<UserDto, UserEntity>();
            cfg.CreateMap<FriendDto, UserEntity>();

            cfg.CreateMap<OldUsername, UsernameEntity>();
        });
    }
}