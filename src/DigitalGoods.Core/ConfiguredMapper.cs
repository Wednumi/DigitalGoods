using AutoMapper;
using DigitalGoods.Core.Extensions;

namespace DigitalGoods.Core
{
    public static class ConfiguredMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = GetConfiguredMapper<TSource, TDestination>();
            return mapper.Map<TDestination>(source);
        }

        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var mapper = GetConfiguredMapper<TSource, TDestination>();
            mapper.Map(source, destination);
        }

        private static Mapper GetConfiguredMapper<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()
                .IgnoreNoMap());
            return new Mapper(config);
        }
    }
}
