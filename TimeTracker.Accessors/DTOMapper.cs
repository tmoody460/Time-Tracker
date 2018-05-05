using AutoMapper;

namespace TimeTracker.Accessors
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
                        cfg.CreateMap<EntityFramework.Log, Accessors.Models.Log>();
                        cfg.CreateMap<Accessors.Models.Log, EntityFramework.Log>();
                        cfg.CreateMap<Accessors.Models.Activity, EntityFramework.Activity>();
                        cfg.CreateMap<EntityFramework.Activity, Accessors.Models.Activity>();
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