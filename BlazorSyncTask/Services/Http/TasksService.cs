using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services.Http;


public class TasksService: ITasksService
{
    private readonly HttpClient client;

    public TasksService(HttpClient client)
    {
        this.client = client;
    }


    public async Task CreateTask(CreateTaskDto createTaskDto)
    {
        
        string taskAsJson = JsonSerializer.Serialize(createTaskDto);
     
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/api/tasks/", content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
      
    }

    public async Task<List<TaskDTO>> GetAllTasksByUserId(int userId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/tasks/byUser/"+userId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        List<TaskDTO> pendingUsers = JsonSerializer.Deserialize<List<TaskDTO>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return pendingUsers;
    }

    public async Task DeleteTask(long taskId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.DeleteAsync("http://localhost:8080/api/tasks/"+taskId);
        string responseContent = await response.Content.ReadAsStringAsync();
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<TaskDTO> GetTaskById(int taskId)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/tasks/" + taskId);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        TaskDTO task = JsonSerializer.Deserialize<TaskDTO>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
        return task;
    }

    public async Task EditTaskById(CreateTaskDto task, int userId)
    {
        string taskAsJson = JsonSerializer.Serialize(task);
     
        StringContent content = new(taskAsJson, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await client.PutAsync("http://localhost:8080/api/tasks/"+userId, content);
        string responseContent = await response.Content.ReadAsStringAsync();
     
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
       
    }
}