using System.Security.Claims;
using Shared.Models;

namespace BlazorSyncTask.Services;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    Task RegisterAsync(string fullName, string userName, string password,string email);
}
