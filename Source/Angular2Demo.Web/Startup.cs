using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Angular2Demo.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
				.AddJsonFile("config.json") // Make sure to configure the database info in this file!
				.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
		{
			// CORS Setup
			services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyMethod().WithHeaders("accept", "authorization", "content-type", "origin", "x-custom-header").AllowCredentials()));

			// EF / Database Setup
			services.AddEntityFramework().AddSqlServer().AddDbContext<Angular2DemoContext>(x => x.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));


			// Add framework services.
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
				options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=App}/{action=Index}/{id?}");
			});
		}

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
