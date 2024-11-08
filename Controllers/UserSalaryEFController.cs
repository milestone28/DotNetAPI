using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    DataContextEf _entityFramework;

    public UserSalaryEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEf(config);
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {
        IEnumerable<UserSalary> userssalary = _entityFramework.UserSalary.ToList<UserSalary>();
        return userssalary;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary.Where(x => x.UserId == userId).FirstOrDefault<UserSalary>();
        if (userSalary != null)
        {
            return userSalary;
        }

        throw new Exception("Failed to Get User");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework
            .UserSalary.Where(u => u.UserId == userSalary.UserId)
            .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            userSalaryDb.UserId = userSalary.UserId;
            userSalaryDb.Salary = userSalary.Salary;
            userSalaryDb.AvgSalary = userSalary.AvgSalary;
             _entityFramework.SaveChanges();
             return Ok();
        }
        throw new Exception("Failed to Update UserSalary");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUser(UserSalary userSalary)
    {
        UserSalary? userSalaryDB = new UserSalary();
        userSalaryDB.UserId = userSalary.UserId;
        userSalaryDB.Salary = userSalary.Salary;
        userSalaryDB.AvgSalary = userSalary.AvgSalary;
        _entityFramework.Add(userSalaryDB);

        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Create UserSalary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary.Where(x => x.UserId == userId).FirstOrDefault<UserSalary>();
        if (userSalary != null)
        {
            _entityFramework.UserSalary.Remove(userSalary);
        }
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Delete UserSalary");
    }
}
