using MANAGER.Backend.WebApi;
using MANAGER.Backend.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLog();

var startup = new Startup(builder.Configuration);

startup.ConfigurationServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

public partial class Program { }