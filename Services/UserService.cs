﻿using Data;
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
        foreach (var course in context.Courses.ToList())
        {
            var courseId = course.Id;
            context.UserCourses.Add(new User_Course() {Course_Id = courseId, User_Id = newUser.Id, Progress = 0});
        }
        foreach (var module in context.Modules)
        {
            var moduleId = module.Id;
            context.UserModules.Add(new User_Modules() {Module_Id = moduleId, User_Id = newUser.Id});
        }
        context.SaveChanges();
    }
    
    public UserEntity Get(int id)
    {
        return context.Users.First(x => x.Id == id);
    }
    public UserEntity Get(string pas, string name)
    {
        return context.Users.First(x => x.Password_HK == pas && x.Name == name);
    }
}