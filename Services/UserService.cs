using Data;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Models.Model;

namespace Services;

public class UserService : IUserService
{
    private AppDbContext context;
    
    public UserService(AppDbContext context)
    {
        this.context = context;
    }
    public UserEntity Add(UserDto user)
    {
        var newUser = new UserEntity
        {
            Name = user.Name,
            Mail = user.Mail,
            Password_HK = user.Password_HK,
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(newUser);
        context.SaveChanges();
        foreach (var course in context.Courses.ToList())
        {
            var courseId = course.Id;
            context.UserCourses.Add(new User_Course {Course_Id = courseId, User_Id = newUser.Id});
        }
        foreach (var module in context.Modules)
        {
            var moduleId = module.Id;
            context.UserModules.Add(new User_Modules {Module_Id = moduleId, User_Id = newUser.Id});
        }
        context.SaveChanges();
        return newUser;
    }
    
    public UserEntity? Get(int id)
    {
        return context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }
    public UserEntity? Get(string pas, string mail)
    {
        return context.Users.AsNoTracking().FirstOrDefault(x => x.Password_HK == pas && x.Mail == mail);
    }
}