using Application.Common.Jwt;
using Domain.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, JwtTokenOption jwtTokenOption)
        {
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

            return services;
        }
    }
}
