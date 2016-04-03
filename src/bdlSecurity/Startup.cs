using AutoMapper;
using bdlSecurity.Models;
using bdlSecurity.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using System;

namespace bdlSecurity
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var authDefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddMvc(config =>
            {
                config.Filters.Add(new AuthorizeFilter(authDefaultPolicy));
            //#if !DEBUG
            //        config.Filters.Add(new RequireHttpsAttribute());
            //#endif
            })
                .AddJsonOptions(opt => { opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            services.AddEntityFramework().AddSqlServer().AddDbContext<SecurityContext>();

            services.AddLogging();

            //services.AddIdentity()  //<BadgeUser, BadgeUserRole>()
            //    .AddEntityFrameworkStores<BadgeUserDbContext>()
            //    .AddDefaultTokenProviders();

            // injection inventory
            services.AddScoped<IRequestViewModel, RequestViewModel>();
            services.AddScoped<IBadgeViewModel, BadgeViewModel>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddSingleton<BadgeUserManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(LogLevel.Debug);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                loggerFactory.AddConsole(LogLevel.Error);
                app.UseExceptionHandler("/Home/Error"); 
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            //app.UseIdentity();

            //app.UseExceptionHandler("/Home/Error");

            //app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            int timeout = -1;
            if (int.TryParse(Configuration["AppConfig:AppTimeoutInMinutes"], out timeout) == false)
                timeout = 20;   // default of 20  mins
            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "ApplicationCookie";
                options.LoginPath = new PathString("/Account/Login");
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(timeout);
                options.SlidingExpiration = true;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            // client to server object mapping configuration
            Mapper.Initialize(config =>
            {
                // bi-directional object map
                config.CreateMap<tb_request, BadgeRequestViewModel>().ReverseMap();
                config.CreateMap<tb_request_escort, BadgeRequestEscortViewModel>().ReverseMap();

            });

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
