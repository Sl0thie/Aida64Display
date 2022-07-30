// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-6.0&tabs=visual-studio
using System.Net;

using Aida64Service;
using Aida64Service.Hubs;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting.WindowsServices;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Aida64Service - .txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

WebApplicationOptions? options = new ()
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
};

WebApplicationBuilder? builder = WebApplication.CreateBuilder(options);
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddHostedService(provider =>
{
    IHubContext<DataHub>? hubContext = provider.GetService<IHubContext<DataHub>>();
    Worker? aWorker = new (hubContext);
    return aWorker;
});

builder.WebHost.ConfigureKestrel(configureOptions: (context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Parse("192.168.0.6"), 929);
});

builder.Host.UseWindowsService();
WebApplication? app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapHub<DataHub>("/dataHub");
await app.RunAsync();