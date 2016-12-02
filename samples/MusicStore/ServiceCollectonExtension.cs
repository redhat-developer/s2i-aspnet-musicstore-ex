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
            var providerType = configuration[configuration["Data:Provider:Type"]].ToLower();
            var connectionString = configuration[configuration["Data:Provider:Connection"]];

            switch (providerType)
            {
                case "default":
                    var platform = new Platform();
                    if (platform.UseInMemoryStore)
                    {
                        services.AddDbContext<MusicStoreContext>(options => options.UseInMemoryDatabase());
                    }
                    else
                    {
                        services.AddDbContext<MusicStoreContext>(options => options.UseSqlServer(connectionString));
                    }
                    break;

                case "sqlserver":
                    services.AddDbContext<MusicStoreContext>(options => options.UseSqlServer(connectionString));
                    break;

                case "inmemory":
                    services.AddDbContext<MusicStoreContext>(options => options.UseInMemoryDatabase());
                    break;

                case "pgsql":
                    services.AddDbContext<MusicStoreContext>(options => options.UseNpgsql(connectionString));
                    break;

                case "mariadb":
                    services.AddDbContext<MusicStoreContext>(options => options.UseSqlite(connectionString));
                    break;

                case "sqlite":
                    services.AddDbContext<MusicStoreContext>(options => options.UseMySql(connectionString));
                    break;

                case "mysql":
                    services.AddDbContext<MusicStoreContext>(options => options.UseMySql(connectionString));
                    break;

                default:
                    throw new ArgumentException("unkown provider", providerType);
            };

            return services;
        }
    }
}