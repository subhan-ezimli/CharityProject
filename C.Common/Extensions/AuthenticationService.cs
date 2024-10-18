using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApp.Extensions;




public static class AuthenticationService
{
    public static IServiceCollection AddAuthenticationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opts =>
        {
            opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    ValidateAudience = false
                };
                cfg.IncludeErrorDetails = true;

            });

        return services;
    }
}
