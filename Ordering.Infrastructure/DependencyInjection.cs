using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Odering.Core.Repositories.Command;
using Odering.Core.Repositories.Command.Base;
using Odering.Core.Repositories.Query;
using Odering.Core.Repositories.Query.Base;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Identity;
using Ordering.Infrastructure.Repositories.Command;
using Ordering.Infrastructure.Repositories.Command.Base;
using Ordering.Infrastructure.Repositories.Query;
using Ordering.Infrastructure.Repositories.Query.Base;
using Ordering.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderingContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(OrderingContext).Assembly.FullName)
                ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<OrderingContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });


            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddTransient<ICustomerQueryRepository, CustomerQueryRepository>();
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<ICustomerCommandRepository, CustomerCommandRepository>();


            return services;
        }
    }
}
