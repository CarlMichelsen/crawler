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
            cfg.CreateMap<ProfileDto, StatsEntity>()
                .ForMember(s => s.Id, opt => opt.Ignore());
            cfg.CreateMap<ProfileDto, RecentStatsEntity>()
                .ForMember(s => s.Id, opt => opt.Ignore());
            cfg.CreateMap<UserDto, UserEntity>();
            cfg.CreateMap<FriendDto, UserEntity>();
            cfg.CreateMap<OldUsername, UsernameEntity>();
        });
    }
}