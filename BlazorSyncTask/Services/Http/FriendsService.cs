using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services.Http;


public class FriendsService: IFriendsService
{
    private readonly HttpClient client;

    public FriendsService(HttpClient client)
    {
        this.client = client;
    }
    
  
    public async Task<List<GetUserDto>> GetAllFriends(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:5041/Friend/getAllFriends?userId=" + userId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        List<GetUserDto> friends = JsonSerializer.Deserialize<List<GetUserDto>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return friends;
    }

    public async Task AddFriend(int userId, int friendId)
    {
        AddFriendDto addFriendDto = new()
        {
            IsAccepted = false,
            UserId = userId,
            FriendId = friendId
        };

        string userAsJson = JsonSerializer.Serialize(addFriendDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:5041/Friend/addFriend", content);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

    

    }
    
    public User GetFriendId(int friendId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GetUserDto>> GetAllUsers()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:5041/Friend/getAllUsers");
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        List<GetUserDto> allUsers = JsonSerializer.Deserialize<List<GetUserDto>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return allUsers;
    }
}