using Workflow.Greeting.Dto;
using Workflow.Infrastructure;

using WorkflowCore.Interface;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<WorkflowMiddleware>();

builder.Services.AddWorkflows();
builder.Services.AddSteps();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWorkflow();
}

app.UseHttpsRedirection();

app.MapGet("/run-workflow", (IWorkflowHost host, IServiceProvider serviceProvider) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<HelloDto>)).Cast<IWorkflow<HelloDto>>();

    foreach (var workflow in workflows)
    {
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");
        host.StartWorkflow(workflow.Id, new HelloDto { Input = "Hi," });
    }
})
.WithName("RunWorkflow");

app.MapGet("/stop-workflow", (IWorkflowHost host) =>
{
    Console.WriteLine("Workflow host stopped!");
    host.Stop();
})
.WithName("StopWorkflow");

app.Run();