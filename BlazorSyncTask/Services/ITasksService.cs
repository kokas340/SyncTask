using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface ITasksService
{
   
    Task CreateTask(CreateTaskDto createTaskDto);

    Task<List<TaskDTO>> GetAllTasksByUserId(int userId);
  
}