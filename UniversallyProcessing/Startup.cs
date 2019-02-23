using System;
using System.Threading.Tasks;
using App.Metrics.Health;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversallyProcessing.Api.Models;
using UniversallyProcessing.Api.Models.DatabaseContext;


namespace UniversallyProcessing.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            Core.Startup.ConfigureServices(services, connectionString);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Startup>());

            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
            services.AddSignalR();
            services.AddAutoMapper();

            var metrics = AppMetricsHealth.CreateDefaultBuilder()
                .HealthChecks.RegisterFromAssembly(services)
                .BuildAndAddTo(services);

            services.AddHealth(metrics);
            services.AddHealthEndpoints();
            services.AddMemoryCache();
            
            services.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<MainContext>().Database.Migrate();
            }


            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHealthAllEndpoints();
            
            //  CreateUserRoles(serviceProvider).GetAwaiter().GetResult();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!(await roleManager.RoleExistsAsync("User").ConfigureAwait(false)))
            {
                await roleManager.CreateAsync(new IdentityRole("User")).ConfigureAwait(false);
            }
        }
    }
}
