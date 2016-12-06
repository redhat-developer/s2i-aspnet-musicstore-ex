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
            string providerType = configuration["DATABASE_TYPE"];
            string connectionString = null;

            if (string.IsNullOrEmpty(providerType))
            {
                providerType = configuration[configuration["Data:Provider:Type"]].ToLower();
                connectionString = configuration[configuration["Data:Provider:Connection"]];
            }
            else
            {
                connectionString = GetConnectionStringFromEnvironment(configuration, providerType.ToLower());
            }

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

                case "sqlite":
                    services.AddDbContext<MusicStoreContext>(options => options.UseSqlite(connectionString));
                    break;

                case "mariadb":
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

        public static string GetConnectionStringFromEnvironment(IConfiguration configuration, string providerType)
        {
            string connectionString = null;

            if (string.IsNullOrEmpty(providerType))
            {
                return connectionString;
            }

            var databaseName = configuration["DATABASE_NAME"];
            var databaseHost = configuration["DATABASE_HOST"];
            var databasePort = configuration["DATABASE_PORT"];
            var databaseUsername = configuration["DATABASE_USERNAME"];
            var databasePassword = configuration["DATABASE_PASSWORD"];

            if (providerType.Contains("inmemory"))
            {
                // do nothing
            }

            else if (providerType.Contains("sqlserver"))
            {
                connectionString = string.Format("Server=(localdb)\\MSSQLLocalDB;Database={0};Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;", databaseName);
            }

            else if (providerType.Contains("sqlite"))
            {
                connectionString = string.Format("data source={0}.db;", databaseName);
            }

            else if (providerType.Contains("pgsql"))
            {
                connectionString = string.Format("host={0};port={1};database={2};username={3};password={4}",
                    databaseHost, databasePort, databaseName, databaseUsername, databasePassword);
            }

            else if (providerType.Contains("mariadb") || providerType.Contains("mysql"))
            {
                connectionString = string.Format("server={0};port={1};database={2};uid={3};pwd={4};",
                    databaseHost, databasePort, databaseName, databaseUsername, databasePassword);
            }

            return connectionString;
        }
    }
}
