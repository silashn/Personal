using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pages.Configuration.Database;
using Pages.Data.Contexts;
using System;

namespace Pages.Web
{
    public class Startup
    {
        public IConfiguration config { get; }

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "../../../")
                .AddJsonFile("Appsettings/appsettings.json", false, true)
                .AddJsonFile($"AppSettings/appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            Configurator.ConfigureServices(services, config);
            services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(options =>
            {
                options.MapRoute("Default", "{controller}/{action}/{id?}");
            });
        }
    }
}
