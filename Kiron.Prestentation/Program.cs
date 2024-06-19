using Microsoft.OpenApi.Models;
using Kiron.Persistence;
using Kiron.Infrastructure;
using Kiron.CacheService;
using Kiron.Application;
using Kiron.Infrastructure.ThirdPartyHoliday;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Kiron.Presentation.Middleware;
using Kiron.Application.Settings;
using Microsoft.Extensions.Configuration;

namespace Kiron.Prestentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
    

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Auth header using Bearer Scheme",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        builder.Services.AddPersistenceServices();
        builder.Services.AddRepositoryServices(builder.Configuration);
        builder.Services.AddCacheServices();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddThirdPartyHolidayServices(builder.Configuration);

   
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kiron App");
            c.RoutePrefix = string.Empty;
        });

        app.MapControllers();
        app.UseMiddleware<ExceptionHandlingMiddleWare>();

        app.Run();
    }
}
