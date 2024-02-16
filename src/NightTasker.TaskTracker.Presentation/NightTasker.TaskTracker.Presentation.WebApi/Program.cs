using NightTasker.Common.Core.Exceptions.Middlewares;
using NightTasker.TaskTracker.Core.Application.Configuration;
using NightTasker.TaskTracker.Infrastructure.Messaging.Configuration;
using NightTasker.TaskTracker.Infrastructure.Persistence.Configuration;
using NightTasker.TaskTracker.Presentation.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .RegisterApplicationServices()
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterMessagingServices(builder.Configuration)
    .RegisterApiServices(builder.Configuration);

builder.Services.ConfigureControllers();

var app = builder.Build();

await app.ApplyDatabaseMigrations(CancellationToken.None);

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;