using System.Net;

using Aida64Service;
using Aida64Service.Hubs;
using Microsoft.Extensions.Hosting.WindowsServices;

// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-6.0&tabs=visual-studio


WebApplicationOptions? options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddHostedService<Worker>();

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen( IPAddress.Parse("192.168.0.6") , 929);  
});

builder.Host.UseWindowsService();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapHub<DataHub>("/dataHub");
await app.RunAsync();