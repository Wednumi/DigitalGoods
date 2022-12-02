using AutoMapper;
using Azure.Core;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public static class ModelMapper
    {
        public static TResult Map<TSource, TResult>(TSource source)
        {
            var mapper = GetConfiguredMapper<TSource, TResult>();
            return mapper.Map<TResult>(source);
        }

        public static void Map<TSource, TResult>(TSource source, TResult destination)
        {
            var mapper = GetConfiguredMapper<TSource, TResult>();
            mapper.Map(source, destination);
        }

        private static Mapper GetConfiguredMapper<TSource, TResult>()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TResult>());
            return new Mapper(config);
        }
    }
}
