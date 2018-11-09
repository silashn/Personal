using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Configuration.Services;
using System;
using System.IO;

namespace Playground.Web
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(System.AppContext.BaseDirectory)
                            .AddJsonFile(AppContext.BaseDirectory + "..\\..\\..\\..\\Playground.Configuration\\Appsettings\\appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile(AppContext.BaseDirectory + $"..\\..\\..\\..\\Playground.Configuration\\Appsettings\\appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();

            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            ServiceConfigurator.ConfigureServices(services, configuration);
            services.BuildServiceProvider();

            //Configure View location formats, to allow for specific folder/view structures.
            services.Configure<RazorViewEngineOptions>(o =>
            {
                //{1} = Controller
                //{0} = Action
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);

                //Look subfolders inside Views/ folder (Admin/, Home/, Shared/, etc.).
                foreach(DirectoryInfo dir in new DirectoryInfo("Views/").GetDirectories())
                {
                    //Look for subfolders inside the parent subfolder (Admin/Membership/, Admin/Shop/, etc.).
                    foreach(DirectoryInfo subDir in dir.GetDirectories())
                    {
                        //Add the corresponding ViewLocationFormat, as a valid way of looking for views (Admin/Membership/Authors, Admin/Shop/Books, etc.).
                        o.ViewLocationFormats.Add("/Views/{1}/" + subDir.Name + "/{0}" + RazorViewEngine.ViewExtension);
                        o.ViewLocationFormats.Add(("/Views/Admin/" + subDir.Name + "/{0}") + RazorViewEngine.ViewExtension);
                    }
                }
                o.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseExceptionHandler("/Home/Error/");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
