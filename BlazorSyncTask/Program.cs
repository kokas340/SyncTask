using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorSyncTask;
using BlazorSyncTask.Auth;
using BlazorSyncTask.Services;
using BlazorSyncTask.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Shared.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<IFriendsService, FriendsService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<IWebSocketService, WebSocketService>();
builder.Services.AddScoped<DialogService>();

AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services
        .AddBlazorise( options =>
        {
            options.Immediate = true;
        })
        .AddBootstrapProviders()
       .AddFontAwesomeIcons();
await builder.Build().RunAsync();