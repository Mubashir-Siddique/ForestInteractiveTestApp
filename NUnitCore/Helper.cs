using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;
using ForestInteractiveTestApp.Repository;

namespace ForestInteractiveTestApp.Common
{
    public class Helper
    {
        private static IServiceCollection _services = null;
        private static IServiceProvider _serviceProvider = null;

        public static void InitConfig()
        {
            if (APIConfig.Configuration == null)
            {
                var myConfiguration = new Dictionary<string, string>
                        {
                            {"ConnectionStrings:DefaultConnection", "Server=DESKTOP-KRMF9V2\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True; Integrated Security = true;"},
                        };
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(myConfiguration)
                    .Build();
                APIConfig.Configuration = configuration;
            }
        }

        public static T GetService<T>()
        {

            if (_services == null)
            {
                var services = new ServiceCollection();

                services.AddSingleton<IConnection, ConnectionFactory>();
                services.AddScoped<IRepository<Schedule>, GenericRepository<Schedule>>();

                _serviceProvider = services.BuildServiceProvider();
            }


            var _service = _serviceProvider.GetService<T>();
            return _service;
        }

    }
}
