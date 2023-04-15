using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Config;
using Ordering.Infrastructure.Services;
using System.Text;

namespace Ordering.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddAuthen(this IServiceCollection services,
            JwtSettings jwtSettings)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(jwtSettings.ExpiryMinutes))

                };
            });
            // Dependency injection with key
            services.AddSingleton<ITokenGenerator>(new TokenGenerator(jwtSettings.Key, jwtSettings.Issuer, jwtSettings.Audience, jwtSettings.ExpiryMinutes));

            return services;
        }
    }
}
