using Application.Common;
using Application.Common.Behaviours;
using Application.Common.Jwt;
using Application.Common.Settings;
using Application.MonitorApis;
using Domain.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
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

            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();
            monitorApiServiceSetting.Guard(nameof(monitorApiServiceSetting));
            monitorApiServiceSetting.Guard();

            var applicationSetting = configuration
                .GetSection(nameof(ApplicationSetting)).Get<ApplicationSetting>();
            applicationSetting.Guard(nameof(applicationSetting));
            applicationSetting.Guard();

            services.AddHttpClient<MonitorApiService>()
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        MaxConnectionsPerServer = 1,
                    };
                });

            services.AddScoped<ApplicationUser>()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionLogBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
