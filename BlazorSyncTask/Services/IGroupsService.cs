using Shared.Dtos;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IGroupsService
{
   
    Task CreateGroup(CreateGroupDto createGroupDto);
    Task<List<GroupDTO>> GetAllGroupsByUserId(int userId);
    Task<GroupDTO> GetGroupById(int groupId);
  
}