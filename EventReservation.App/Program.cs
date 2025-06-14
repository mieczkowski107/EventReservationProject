using EventReservation.App.Services;
using EventReservation.App.Services.Interfaces;
using EventReservation.DataAccess;
using EventReservation.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EventReservation.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging());

        // Registering services
        builder.Services.AddSingleton<TokenProvider>();
        builder.Services.AddTransient<DbSeeder>();
        builder.Services.AddScoped<IOverlappingService, OverlappingService>();

        //Registering Identity services
        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                }
            )
            .AddEntityFrameworkStores<AppDbContext>();


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // No toleration for tokens that are in the past
                };
            });
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        //Swagger configuration

        /*  builder.Services.AddSwaggerGen(options =>
          {
              options.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Reservation API", Version = "v1" });

          });*/

        // Uncomment the following lines to enable Swagger with JWT authentication and comment out the above AddSwaggerGen call

        builder.Services.AddSwaggerGen(option =>
          {
              option.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Reservation API", Version = "v1" });
              option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
              {
                  In = ParameterLocation.Header,
                  Description = "Please enter a valid token",
                  Name = "JWT Authorization",
                  Type = SecuritySchemeType.Http,
                  BearerFormat = "JWT",
                  Scheme = JwtBearerDefaults.AuthenticationScheme
              });
              option.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                  {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type=ReferenceType.SecurityScheme,
                              Id=JwtBearerDefaults.AuthenticationScheme
                          }
                      },
                      new string[]{}
                  }
              });
          });
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DisplayRequestDuration();

            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<DbSeeder>();
            seeder.Seed();
        }

        app.Run();

       
    }
}
