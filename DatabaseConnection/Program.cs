using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString["Default"]));

app.MapGet("/", () => "Hello World!");

app.Run();