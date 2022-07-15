using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TremendBoard.Infrastructure.Services;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.HostedServices;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace TremendBoard.Mvc
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
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddSingleton(Configuration);

            services.AddRepository(
                            Configuration.GetConnectionString("SqlConnectionString"),
                            Configuration.GetValue<int>("DbConnectionMaxRetryCount"),
                            Configuration.GetValue<int>("DbConnectionMaxRetryDelay"));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
            services.AddHealthChecks();
            services.AddSwaggerGen();

            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(Configuration.GetConnectionString("DBConnection"), new Hangfire.SqlServer.SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                });
            });
            services.AddHangfireServer();

            // Register services.
            services.AddTransient<IMailingService, MailingService>();
            services.AddTransient<IProjectRepository, ProjectRepository>();

            // Registerd background/hosted services.
            services.AddHostedService<SendEmailHostedService>();
            services.AddHostedService<SendEmailBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });

            app.UseHangfireDashboard();
        }
    }
}
