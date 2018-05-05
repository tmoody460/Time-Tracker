using AutoMapper;

namespace TimeTracker.Managers
{
    internal static class DTOMapper
    {
        static IMapper _mapper;
        private static IConfigurationProvider _config;

        static IMapper Mapper => _mapper ?? (_mapper = Configuration.CreateMapper());

        public static IConfigurationProvider Configuration
        {
            get
            {
                if (_config == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Managers.Models.Log, Accessors.Models.Log>()
                            .ForMember(a => a.Activity, b => b.Ignore());

                        cfg.CreateMap<Accessors.Models.Log, Managers.Models.Log>();

                        cfg.CreateMap<Accessors.Models.Log, Managers.Models.LogWithActivity>();

                        cfg.CreateMap<Accessors.Models.Activity, Managers.Models.Activity>();

                        cfg.CreateMap<Managers.Models.Activity, Accessors.Models.Activity>();
                    });
                    _config = config;
                }
                return _config;
            }
        }

        public static void Map(object source, object dest)
        {
            Mapper.Map(source, dest, source.GetType(), dest.GetType());
        }

        public static T MapTo<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}