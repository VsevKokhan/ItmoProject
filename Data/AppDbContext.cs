using Microsoft.EntityFrameworkCore;
using Models;
using Models.Model;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<User_Course> UserCourses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<User_Modules> UserModules { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User_Course>()
            .HasKey(uc => new { uc.User_Id, uc.Course_Id });
        modelBuilder.Entity<User_Modules>()
            .HasKey(um => new { um.User_Id, um.Module_Id });
        modelBuilder.Entity<User_Modules>()
            .Property(um => um.Is_Passed).HasDefaultValue(false);


    }
}