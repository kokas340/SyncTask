using Shared.Models;

namespace EfcDataAccess.DAOs;

public class TaskEfcDao
{
    private readonly AsyncTaskContext context;

    public TaskEfcDao(AsyncTaskContext context)
    {
        this.context = context;
    }
    public Task<UserT> CreateTask(Tasks task)
    {
        throw new NotImplementedException();
    }

    //public Task<IEnumerable<User>> GetUser(SearchTodoParametersDto searchParameters)
    //{
   //     throw new NotImplementedException();
   // }

    public Tasks UpdateTask(Tasks task)
    {
        throw new NotImplementedException();
    }

    public Task<Tasks?> GetByIdTask(int taskId)
    {
        throw new NotImplementedException();
    }

    public Tasks DeleteTask(int id)
    {
        throw new NotImplementedException();
    }
}
