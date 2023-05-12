using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using BlazorSyncTask.Services.Http;
using Microsoft.IdentityModel.Tokens;

using EfcDataAccess;
using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Npgsql;
using Shared.Dtos;
using Shared.Models;

using WebAPI.Services;

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
            
            Friends friend = await connectionDB.AddFriendAsync( dto.UserId,user,false);
            return Ok(friend);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

       
    }



}