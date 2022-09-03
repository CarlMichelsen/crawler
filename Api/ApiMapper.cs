using AutoMapper;
using Database.Entities;
using Api.Dto;

public static class ApiMapper
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
            cfg.CreateMap<ProfileEntity, ProfileDto>();
            cfg.CreateMap<StatsEntity, StatsDto>();
            cfg.CreateMap<RecentStatsEntity, RecentStatsDto>();
            
            cfg.CreateMap<UsernameEntity, string>()
                .ConvertUsing(src => src.Username);

            cfg.CreateMap<UserEntity, string>()
                .ConvertUsing(src => src.Username);
        });
    }
}