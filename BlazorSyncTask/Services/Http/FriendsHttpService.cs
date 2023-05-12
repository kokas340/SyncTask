using System.Net.Http.Json;
using Shared.Models;

namespace BlazorSyncTask.Services.Http;

public class FriendsHttpService : IFriendsInterface
{
    private readonly HttpClient _httpClient;

    public FriendsHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddFriendAsync(int userId, int friendId)
    {
        await _httpClient.PostAsJsonAsync<Friends>($"api/friends/add/{userId}/{friendId}", null);
    }

    public async Task RemoveFriendAsync(int userId, int friendId)
    {
        await _httpClient.DeleteAsync($"api/friends/remove/{userId}/{friendId}");
    }

    public async Task<Friends> GetFriendsAsync(int userId, int friendId)
    {
        return await _httpClient.GetFromJsonAsync<Friends>($"api/friends/get/{userId}/{friendId}");
    }

    public async Task<List<User>> GetFriendsListAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<List<User>>($"api/friends/list/{userId}");
    }

    public async Task<User> GetFriendByUsernameAsync(int userId, string username)
    {
        return await _httpClient.GetFromJsonAsync<User>($"api/friends/username/{userId}/{username}");
    }
}