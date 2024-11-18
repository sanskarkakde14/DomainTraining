using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleFly_BookingSystem.Data;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Services;
using SimpleFly_BookingSystem.Authentication;
using System.Text;
using System.Text.Json.Serialization;

namespace SimpleFly_BookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            builder.Services.AddDbContext<SimplyFlyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var jwtSettings = configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettings);
            builder.Services.AddScoped<JwtTokenGenerator>();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("FlightOwnerPolicy", policy => policy.RequireRole("FlightOwner"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFlightRouteService, FlightRouteService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Enable ReferenceHandler.Preserve to prevent circular reference errors
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;


                    // Optionally, increase the maximum depth of serialized objects
                    options.JsonSerializerOptions.MaxDepth = 64;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleFly Booking API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Enter 'Bearer' followed by a space and then your JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();      
            app.MapControllers();
            app.Run();
        }
    }
}