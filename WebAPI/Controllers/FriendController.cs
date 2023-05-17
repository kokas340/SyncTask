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
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;
    private static AsyncTaskContext _context = new AsyncTaskContext();
    private FriendsEfcDao _connectionDb = new FriendsEfcDao(_context);
    private UserEfcDao _connectionDbUser = new UserEfcDao(_context);
    public FriendController(IConfiguration config, IAuthService authService)
    {
        this._config = config;
        this._authService = authService;
    }
    
    [HttpPost, Route("addFriend")]

    public async Task<ActionResult> AddFriend([FromBody] AddFriendDto dto)
    {
        try
        {
            User user = await _connectionDbUser.GetByIdAsync(dto.FriendId);
            
            GetUserDto userdto = new GetUserDto()
            {
                fullName = user.FullName,
                email = user.Email,
                id = user.Id,
                username = user.Username
            };
            Friends friend = await _connectionDb.AddFriendAsync( dto.UserId,userdto);
            return Ok(friend);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAllFriends")]
    [Authorize]
    public async Task<ActionResult> GetAllFriends(int userId)
    {
        try
        {
            List<GetUserDto> listFriends = await _connectionDb.GetFriendsAsync(userId);
            return Ok(listFriends);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet, Route("getFriend")]
    [Authorize]
    public async Task<ActionResult> GetFriend(int friendId)
    {
        try
        {
            GetUserDto friend = await _connectionDb.GetFriendAsync(friendId);
            return Ok(friend);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAllPendingFriends")]
    [Authorize]
    public async Task<ActionResult> GetAllPendingFriends(int userId)
    {
        try
        {
            List<GetFriendsDto> listFriends = await _connectionDb.GetAllPendingFriendsAsync(userId);
            return Ok(listFriends);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAllUsers")]
    [Authorize]
    public async Task<ActionResult> GetAllUsers()
    {
        try
        {
            List<GetUserDto> listFriends = await _connectionDb.GetAllUsers();
            return Ok(listFriends);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("acceptFriendsRequest")]
    [Authorize]
    public async Task<ActionResult> AcceptFriendsRequest(int friendRequestId)
    {
        try
        {
            User user = await _connectionDb.AcceptFriendRequest(friendRequestId);
            
         
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    [HttpDelete("{friendRequestId:int}")]
    public async Task<ActionResult> DeletePendingRequest([FromRoute] int friendRequestId)
    {
        try
        {
            await _connectionDb.DeleteFriend(friendRequestId);
             return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveFriend(int userId, int friendId)
    {
        try
        {
            await _connectionDb.RemoveFriend(userId,friendId);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


}