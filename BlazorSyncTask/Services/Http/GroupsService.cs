using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services.Http;


public class GroupsService: IGroupsService
{
    private readonly HttpClient client;

    public GroupsService(HttpClient client)
    {
        this.client = client;
    }

    

    public async Task CreateGroup(CreateGroupDto createGroupDto)
    {
        string taskAsJson = JsonSerializer.Serialize(createGroupDto);
     
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine("HERE => "+createGroupDto.groupName);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/groups/", content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<List<GroupDTO>> GetAllGroupsByUserId(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/groups/user/" + userId);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        List<GroupDTO> groups = JsonSerializer.Deserialize<List<GroupDTO>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
    
        return groups;
    }

    public async Task<GroupDTO> GetGroupById(int groupId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/groups/" + groupId);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        GroupDTO group = JsonSerializer.Deserialize<GroupDTO>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();

        return group;
    }
}