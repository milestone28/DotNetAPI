using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;

    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {
        string sql =
            @"SELECT [UserId],
            [Salary],
            [AvgSalary] FROM TutorialAppSchema.UserSalary";

        IEnumerable<UserSalary> userssalary = _dapper.LoadData<UserSalary>(sql);
        return userssalary;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql =
            @"SELECT [UserId],
            [Salary],
            [AvgSalary] FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId.ToString();

        UserSalary usersalary = _dapper.LoadDataSingle<UserSalary>(sql);

        return usersalary;
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary usersalary)
    {
        string sql =
            @"UPDATE TutorialAppSchema.UserSalary SET
            [UserId] = '"
            + usersalary.UserId
            + "',[Salary] = '"
            + usersalary.Salary
            + "', [AvgSalary] = '"
            + usersalary.AvgSalary
            + "' WHERE UserId = "
            + usersalary.UserId.ToString();

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update UserSalary");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUser(UserSalary usersalary)
    {
        string sql =
            @"INSERT INTO TutorialAppSchema.UserSalary(
   [UserId],
   [Salary],
   [AvgSalary]
   ) VALUES ("
            + "'"
            + usersalary.UserId
            + "','"
            + usersalary.Salary
            + "','"
            + usersalary.AvgSalary
            + "')";

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Create UserSalary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = (@"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId).ToString();

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete UserSalary");
    }
}
