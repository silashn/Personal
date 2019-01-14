using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
