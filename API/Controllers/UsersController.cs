using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepo,
                           IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }


    /////////////////////////////////////////
    /////////////////////////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var members = await _userRepo.GetMembersAsync();

        return Ok(members);
    }

    /////////////////////////////////////////
    /////////////////////////////////////////
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var member = await _userRepo.GetMemberAsync(username);

        return Ok(member);
    }
}
