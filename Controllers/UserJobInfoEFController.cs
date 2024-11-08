using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    DataContextEf _entityFramework;

    public UserJobInfoEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEf(config);
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
        IEnumerable<UserJobInfo> usersjobinfo = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return usersjobinfo;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        UserJobInfo? userjobinfo = _entityFramework.UserJobInfo.Where(x => x.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (userjobinfo != null)
        {
            return userjobinfo;
        }

        throw new Exception("Failed to Get User");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userjobinfo)
    {
        UserJobInfo? userjobinfoDb = _entityFramework
            .UserJobInfo.Where(u => u.UserId == userjobinfo.UserId)
            .FirstOrDefault<UserJobInfo>();

        if (userjobinfoDb != null)
        {
            userjobinfoDb.UserId = userjobinfo.UserId;
            userjobinfoDb.Department = userjobinfo.Department;
            userjobinfoDb.JobTitle = userjobinfo.JobTitle;
             _entityFramework.SaveChanges();
             return Ok();
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUser(UserJobInfo userjobinfo)
    {
        UserJobInfo? userjobinfoDB = new UserJobInfo();
        userjobinfoDB.UserId = userjobinfo.UserId;
        userjobinfoDB.Department = userjobinfo.Department;
        userjobinfoDB.JobTitle = userjobinfo.JobTitle;
        _entityFramework.Add(userjobinfoDB);

        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Create User");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userjobinfo = _entityFramework.UserJobInfo.Where(x => x.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (userjobinfo != null)
        {
            _entityFramework.UserJobInfo.Remove(userjobinfo);
        }
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Delete UserJobInfo");
    }
}
