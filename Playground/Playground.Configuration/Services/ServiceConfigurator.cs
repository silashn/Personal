using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Repositories;
using Playground.Settings.Database;

namespace Playground.Configuration.Services
{
    public class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            //Appsettings
            services.AddSingleton(config.GetSection("DatabaseSettings").Get<DatabaseSettings>());

            //Services
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }
    }
}
