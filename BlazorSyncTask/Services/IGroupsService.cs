using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IGroupsService
{
   
    Task CreateGroup(CreateGroupDto createGroupDto);
    Task<List<GroupDTO>> GetAllGroupsByUserId(int userId);
    Task<GroupDTO> GetGroupById(int groupId);
    Task<List<GroupInviteDTO>> GetAllGroupInvitesByUserId(int userId);
    Task AcceptInvite(int userId, int invite);
    Task DeclineInvite(int userId, int invite);
    Task CreateTaskGroup(CreateTaskDto createTaskDto, int groupId);
    Task LeaveGroup(int userId, int gorupId);
}