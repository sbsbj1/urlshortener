using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UrlShortener.Config
{
    public static class AuthExtension
    {
        
        public static IServiceCollection  AddAuthExtension(this IServiceCollection services, IConfiguration configuration)
        {

            var secretKey = configuration["SecretKey"];
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:7188",
                        ValidAudience = "https://localhost:7188",
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
                    };
                });
            return services;
        }

    }
}
