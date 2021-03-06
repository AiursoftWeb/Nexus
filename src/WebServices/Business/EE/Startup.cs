﻿using Aiursoft.Archon.SDK.Services;
using Aiursoft.EE.Data;
using Aiursoft.EE.Models;
using Aiursoft.Identity;
using Aiursoft.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aiursoft.EE
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppsContainer.CurrentAppId = configuration["EEAppId"];
            AppsContainer.CurrentAppSecret = configuration["EEAppSecret"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextWithCache<EEDbContext>(Configuration.GetConnectionString("DatabaseConnection"));

            services.AddIdentity<EEUser, IdentityRole>()
                .AddEntityFrameworkStores<EEDbContext>()
                .AddDefaultTokenProviders();

            services.AddAiurMvc();
            services.AddAiursoftIdentity<EEUser>(
                archonEndpoint: Configuration.GetConnectionString("ArchonConnection"),
                observerEndpoint: Configuration.GetConnectionString("ObserverConnection"),
                probeEndpoint: Configuration.GetConnectionString("ProbeConnection"),
                gateEndpoint: Configuration.GetConnectionString("GatewayConnection"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAiurUserHandler(env.IsDevelopment());
            app.UseAiursoftDefault();
        }
    }
}
