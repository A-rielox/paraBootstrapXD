using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepo;

    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }


    /////////////////////////////////////////
    /////////////////////////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _userRepo.GetUsersAsync();

        return Ok(users);
    }

    /////////////////////////////////////////
    /////////////////////////////////////////
    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        var user = await _userRepo.GetUserByUsernameAsync(username);

        return Ok(user);
    }
}
