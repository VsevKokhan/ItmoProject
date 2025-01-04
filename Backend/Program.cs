using System.Text;
using Data;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление необходимых сервисов
            builder.Services.AddControllers();
            
            // Настройка JWT аутентификации
            var jwtSettings = builder.Configuration.GetSection("JWT");
            string key = jwtSettings["Key"];
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            // Настройка подключения к базе данных
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString));

            // Добавление сервисов
            builder.Services.AddScoped<AppDbContext>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IModuleService, ModuleService>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<CourseService>();
            
            // Добавление Swagger (для разработки)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            // Настройка Swagger в режиме разработки
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            // Маппинг контроллеров
            app.MapControllers();

            // Настройка прослушивания на порту 5000 (Kestrel)
            app.Run("http://*:5000"); 

            // Запуск приложения
            app.Run();
        }
    }
}
