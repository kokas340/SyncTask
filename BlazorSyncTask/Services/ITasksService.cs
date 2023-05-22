using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface ITasksService
{
   
    Task CreateTask(CreateTaskDto createTaskDto);

    Task<List<TaskDTO>> GetAllTasksByUserId(int userId);

    Task DeleteTask(int taskId);
    Task<TaskDTO> GetTaskById(int toInt32);
    Task EditTaskById(CreateTaskDto task, int userId);
}