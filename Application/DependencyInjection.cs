using Application.Common;
using Application.Common.Jwt;
using Application.Common.MonitorApi;
using Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenOption = configuration
                .GetSection(nameof(JwtTokenOption)).Get<JwtTokenOption>();

            jwtTokenOption.Guard(nameof(jwtTokenOption));
            jwtTokenOption.Guard();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOption.Key)),

                        ValidateIssuer = true,
                        ValidIssuer = jwtTokenOption.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtTokenOption.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1),
                    };
                });

            services.AddHttpClient<MonitorApiService>();

            services.AddScoped<ApplicationUser>()
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
