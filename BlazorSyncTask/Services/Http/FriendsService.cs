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
           
            UserId = userId,
            FriendId = friendId,
            IsAccepted = false
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
        Console.WriteLine("heyy");
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
    public async Task<List<GetFriendsDto>> GetAllFriendsPending(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:5041/Friend/getAllPendingFriends?userId="+userId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        List<GetFriendsDto> pendingUsers = JsonSerializer.Deserialize<List<GetFriendsDto>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return pendingUsers;
    }

    public async Task AcceptPending(int requestId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:5041/Friend/acceptFriendsRequest?FriendRequestId="+requestId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        
    }

    public async Task DeletePending(int requestId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.DeleteAsync("http://localhost:5041/Friend/"+requestId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task RemoveFriend(int userId, int friendId)
    {
    
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.DeleteAsync("http://localhost:5041/Friend?userId="+userId+"&friendId="+friendId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<GetUserDto> GetFriendById(int friendId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:5041/Friend/getFriend?friendId="+friendId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        GetUserDto friend = JsonSerializer.Deserialize<GetUserDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return friend;
    }
}