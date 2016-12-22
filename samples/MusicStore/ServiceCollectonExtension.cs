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
            var provider = configuration[StoreConfig.DataProviderKey.Replace("__", ":")];
            var connectionString = configuration[StoreConfig.ConnectionStringKey.Replace("__", ":")];
            if (string.IsNullOrEmpty(provider))
            {
                provider = StoreConfig.DataProviderPlatform;
            }
            if (provider == StoreConfig.DataProviderPlatform)
            {
                var platform = new Platform();
                provider = platform.UseInMemoryStore ? StoreConfig.DataProviderMemory : StoreConfig.DataProviderSqlServer;
            }
            switch (provider)
            {
                case StoreConfig.DataProviderMemory:
                    services.AddDbContext<MusicStoreContext>(options =>
                        options.UseInMemoryDatabase());
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
