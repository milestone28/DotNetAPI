using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;

    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
        string sql =
            @"SELECT [UserId], 
                [JobTitle],
                [Department] from TutorialAppSchema.UserJobInfo";

        IEnumerable<UserJobInfo> usersjobinfo = _dapper.LoadData<UserJobInfo>(sql);
        return usersjobinfo;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string sql =
            @"SELECT [UserId], 
                [JobTitle],
                [Department] from TutorialAppSchema.UserJobInfo WHERE UserId = " + userId.ToString();

        UserJobInfo userjobinfo = _dapper.LoadDataSingle<UserJobInfo>(sql);

        return userjobinfo;
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userjobinfo)
    {
        string sql =
            @"UPDATE TutorialAppSchema.UserJobInfo SET
            [UserId] = '"
            + userjobinfo.UserId.ToString()
            + "',[JobTitle] = '"
            + userjobinfo.JobTitle
            + "',[Department] = '"
            + userjobinfo.Department
            + "' WHERE UserId = "
            + userjobinfo.UserId;

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userjobinfo)
    {
        string sql =
            @"INSERT INTO TutorialAppSchema.UserJobInfo(
            [UserId],
   [JobTitle],
   [Department]
   ) VALUES ("
            + "'"
            + userjobinfo.UserId.ToString()
            + "','"
            + userjobinfo.JobTitle
            + "','"
            + userjobinfo.Department
            + "')";

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Create UserJobInfo");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = (@"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = " + userId).ToString();

        Console.WriteLine(sql);
        if (_dapper.ExucuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }
}
