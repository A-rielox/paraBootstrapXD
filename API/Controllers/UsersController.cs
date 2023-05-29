using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    /////////////////////////////////////////
    /////////////////////////////////////////
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userRepo.GetUserByUsernameAsync(username);

        if (user == null) return NotFound();

        _mapper.Map(memberUpdateDto, user);

        if(await _userRepo.SaveAllAsync()) return NoContent();

        return BadRequest("No se pudo actualizar el usuario.");
    }
}
