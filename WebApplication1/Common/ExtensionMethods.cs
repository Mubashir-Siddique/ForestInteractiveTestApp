using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;
using ForestInteractiveTestApp.Repository;

namespace ForestInteractiveTestApp.Common
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IConnection, ConnectionFactory>();

            services.AddSingleton<IRepository<Schedule>, GenericRepository<Schedule>>();

            return services;
        }

        public static string ToJson(this object value)
        {
            if (value == null) 
                return null;

            string json = JsonConvert.SerializeObject(value);
            return json;
        }

        public static T MapTo<T>(this object map)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap(map.GetType(), typeof(T)));
            IMapper mapper = config.CreateMapper();

            T map1 = mapper.Map<T>(map);
            return map1;
        }

    }
}
