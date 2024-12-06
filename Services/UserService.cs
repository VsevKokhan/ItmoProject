using Data;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    public void Add(UserDto user)
    {
        var newUser = new UserEntity
        {
            Name = user.Name,
            Mail = user.Mail,
            Itmo_Id = user.Itmo_Id,
            Itmo_Password = user.Itmo_Password,
            Password_HK = user.Password_HK,
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(newUser);
        context.SaveChanges();
        var courseIdFromQuery = context.Courses.First(x => x.Name == user.CourseType).Id;
        var UserCourse = new User_Course(){Course_Id = courseIdFromQuery, User_Id = newUser.Id, Progress = 0};
        context.UserCourses.Add(UserCourse);
        context.SaveChanges();
    }
    
    public UserEntity Get(int id)
    {
        return context.Users.First(x => x.Id == id);
    }
}