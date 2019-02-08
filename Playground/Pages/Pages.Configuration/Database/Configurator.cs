using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using System;
using System.IO;

namespace Pages.Configuration.Database
{
    public class Configurator
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IThemeRepository, ThemeRepository>();

            //Appsettings
            services.AddDbContext<PagesDbContext>(options => options.UseSqlServer(config.GetValue<string>("DatabaseSettings:ConnectionString").Replace("%ROOTPATH%", Directory.GetParent(AppContext.BaseDirectory + "..\\..\\..\\").FullName), x => x.MigrationsAssembly("Pages.Web")));
        }
    }
}
