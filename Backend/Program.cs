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
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ClockSkew = TimeSpan.Zero // Убираем временную погрешность
                    };

                    // Обработка событий аутентификации
                    options.Events = new JwtBearerEvents
                    {
                        // Обрабатываем вызовы, если токен отсутствует или недействителен
                        OnChallenge = context =>
                        {
                            context.Response.Redirect("https://localhost:443/api/Auth/Refresh"); // Перенаправление при отсутствии токена
                            context.HandleResponse(); // Предотвращаем стандартный ответ
                            return Task.CompletedTask;
                        }
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
