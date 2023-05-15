using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using BlazorSyncTask.Services;
using BlazorSyncTask.Services.Http;
using Microsoft.IdentityModel.Tokens;

using EfcDataAccess;
using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Npgsql;
using Shared.Dtos;
using Shared.Models;



namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
    private static AsyncTaskContext context = new AsyncTaskContext();
    private FriendsEfcDao connectionDB = new FriendsEfcDao(context);
    private UserEfcDao connectionDBUser = new UserEfcDao(context);
    public FriendController(IConfiguration config, IAuthService authService)
    {
        this.config = config;
        this.authService = authService;
    }
    
    [HttpPost, Route("addFriend")]
    public async Task<ActionResult> addFriend([FromBody] AddFriendDto dto)
    {
        try
        {
            User user = await connectionDBUser.GetByIdAsync(dto.FriendId);
            
            GetUserDto userdto = new GetUserDto()
            {
                fullName = user.FullName,
                email = user.Email,
                id = user.Id,
                username = user.Username
                
            };
            Friends friend = await connectionDB.AddFriendAsync( dto.UserId,userdto);
            return Ok(friend);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAllFriends")]
    public async Task<ActionResult> getAllFriends(int userId)
    {
        try
        {
            List<GetUserDto> listFriends = await connectionDB.GetFriendsAsync(userId);
            return Ok(listFriends);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAllPendingFriends")]
    public async Task<ActionResult> getAllPendingFriends(int userId)
    {
        try
        {
            List<User> listFriends = await connectionDB.getAllPendingFriendsAsync(userId);
            return Ok(listFriends);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
  



}