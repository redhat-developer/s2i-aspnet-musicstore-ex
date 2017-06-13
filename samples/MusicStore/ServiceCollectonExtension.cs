using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Models;

namespace MusicStore
{
    static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMusicStoreDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string provider;
            string connectionString;

            // For a nicer example, setting ConnectionString will use PostgreSQL
            if (configuration["ConnectionString"] != null)
            {
                provider = StoreConfig.DataProviderNpgsql;
                connectionString = configuration["ConnectionString"];
            }
            else
            {
                provider = configuration[StoreConfig.DataProviderKey.Replace("__", ":")];
                connectionString = configuration[StoreConfig.ConnectionStringKey.Replace("__", ":")];
            }
            if (string.IsNullOrEmpty(provider))
            {
                provider = StoreConfig.DataProviderPlatform;
            }
            if (provider == StoreConfig.DataProviderPlatform)
            {
                var platform = new Platform();
                provider = platform.UseInMemoryStore ? StoreConfig.DataProviderMemory : StoreConfig.DataProviderSqlServer;
            }

            Console.WriteLine($"Using data provider: {provider}");
            switch (provider)
            {
                case StoreConfig.DataProviderMemory:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseInMemoryDatabase("Scratch"));
                    break;
                case StoreConfig.DataProviderSqlServer:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;
                case StoreConfig.DataProviderNpgsql:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseNpgsql(connectionString));
                    break;
                case StoreConfig.DataProviderSqlite:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
                case StoreConfig.DataProviderMysql:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseMySql(connectionString));
                    break;
                default:
                    throw new ArgumentException("Unknown data provider", StoreConfig.DataProviderKey);
            }
            return services;
        }
    }
}
