using CacheManagement.Infrastucture;
using CacheManagement.Infrastucture.DTOs;
using CacheManagement.Infrastucture.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;

namespace CacheManagement.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            Configure(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var cacheHelper = serviceProvider.GetService<ICacheHelper>();

            MyCustomMessage custom = new MyCustomMessage() { Message = "This is a Test"};
            CacheDTO cache = new CacheDTO(new CultureInfo("PT-BR"), "BR", "TestContext", custom);

            cacheHelper.Set(cache);

            var messager = cacheHelper.Get<MyCustomMessage>(cache);

        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        private static void Configure(IServiceCollection services)
        {
            IConfiguration Configuration = GetConfiguration();

            ConfigureServices(services);

            ConfigureCache(services, Configuration);
        }

        private static void ConfigureCache(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "redis";

            });
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICacheHelper, CacheHelper>();
        }

    }
}
