using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace TremendBoard.Infrastructure.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString, int maxRetryCount, int maxRetryDelay)
        {
            services.AddTransient<IDateTime, SystemDateTime>();
            services.AddTransient<IJobTestService, JobTestService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<TremendBoardDbContext>(options =>
                               options.UseSqlServer(connectionString, x =>
                               {
                                   x.EnableRetryOnFailure(
                                       maxRetryCount,
                                       TimeSpan.FromSeconds(maxRetryDelay),
                                       null
                                   );
                               }), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TremendBoardDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}