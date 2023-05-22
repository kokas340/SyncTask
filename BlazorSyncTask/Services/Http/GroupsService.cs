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
            //throw new Exception(responseContent);
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

        List<GroupDTO> allGroups = JsonSerializer.Deserialize<List<GroupDTO>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();

        List<GroupDTO> acceptedGroups = allGroups
            .Where(group => group.members.Any(member => member.User.id == userId && member.Accepted))
            .ToList();

        return acceptedGroups;
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
    
    public async Task<List<GroupInviteDTO>> GetAllGroupInvitesByUserId(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/groups/user/"+userId+"/invites");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        List<GroupInviteDTO> pendingGroups = JsonSerializer.Deserialize<List<GroupInviteDTO>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return pendingGroups;
    }

    public async Task AcceptInvite(int userId, int invite)
    {
        GroupOptionsDTOP dto = new GroupOptionsDTOP
        {
            userId = userId,
            groupId = invite
        };
        string taskAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/groups/"+invite+"/members/"+userId+"/accept", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task DeclineInvite(int userId, int invite)
    {
        GroupOptionsDTOP dto = new GroupOptionsDTOP
        {
            userId = userId,
            groupId = invite
        };
        string taskAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/groups/"+invite+"/members/"+userId+"/deny", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    
    public async Task CreateTaskGroup(CreateTaskDto createTaskDto,int groupId)
    {
        string taskAsJson = JsonSerializer.Serialize(createTaskDto);
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/groups/"+groupId+"/"+createTaskDto.userId+"/task/", content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    
    public async Task LeaveGroup(int userId, int groupId)
    {
  
        string send = JsonSerializer.Serialize(userId);
        StringContent content = new(send, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/groups/"+groupId+"/members/"+userId+"/leave", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}