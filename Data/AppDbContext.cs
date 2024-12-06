using Microsoft.EntityFrameworkCore;
using Models;
using Models.Model;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<User_Course> UserCourses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка составного первичного ключа для связующей таблицы
        modelBuilder.Entity<User_Course>()
            .HasKey(uc => new { uc.User_Id, uc.Course_Id });
    }
}