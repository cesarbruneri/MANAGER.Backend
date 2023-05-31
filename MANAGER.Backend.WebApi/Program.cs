using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigurationServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

public partial class Program { }