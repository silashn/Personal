using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pages.Data.Models.Membership;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Settings.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Configuration.Database
{
    public class Configurator
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            //Appsettings
            services.AddSingleton(config.GetSection("DatabaseSettings").Get<DatabaseSettings>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IThemeRepository, ThemeRepository>();
        }
    }
}
