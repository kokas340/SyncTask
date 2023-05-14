using System.Net.Http.Headers;
using System.Text.Json;
using Shared.Models;

namespace BlazorSyncTask.Services.Http;


public class FriendsService: IFriendsService
{
    private readonly HttpClient client;

    public FriendsService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<User>> getAllFriends(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("https://localhost:7021/Friend/getAllFriends?userId="+userId);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        IEnumerable<User>? weatherForecasts = JsonSerializer.Deserialize<IEnumerable<User>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return null;
    }

    public User getFriendId(int friendId)
    {
        throw new NotImplementedException();
    }
}