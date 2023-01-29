using Microsoft.AspNetCore.Mvc;

using Workflow.Events;
using Workflow.Events.Dto;
using Workflow.Greeting.Dto;
using Workflow.Infrastructure;
using Workflow.Workflows.Activities;
using Workflow.Workflows.DecisionBranch;
using Workflow.Workflows.ErrorHandling;

using WorkflowCore.Interface;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapPost("/run-workflow", (IWorkflowHost host, IServiceProvider serviceProvider) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<HelloDto>)).Cast<IWorkflow<HelloDto>>();

    foreach (var workflow in workflows)
    {
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");
        host.StartWorkflow(workflow.Id, new HelloDto { Input = "Hi," });
    }

    return Results.Ok();
})
.WithName("RunWorkflow");

app.MapPost("/alarm-workflow", (IWorkflowHost host, IServiceProvider serviceProvider) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<AlarmDto>)).Cast<IWorkflow<AlarmDto>>();

    foreach (var workflow in workflows)
    {
        host.StartWorkflow(workflow.Id, workflow.Version, new AlarmDto { Input = "Hi" });
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");
        host.PublishEvent(AlarmEvent.Name, AlarmEvent.Id, "Sujith", DateTime.Now.AddSeconds(5));
    }

    return Results.Ok();
})
.WithName("AlarmWorkflow");

app.MapPost("/send-email-workflow", (IWorkflowHost host, IServiceProvider serviceProvider) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<SendEmailDto>)).Cast<IWorkflow<SendEmailDto>>();

    foreach (var workflow in workflows)
    {
        host.StartWorkflow(workflow.Id, workflow.Version, new SendEmailDto { Input = "Hi" });
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");

        var activity = host.GetPendingActivity(SendEmailActivity.Name, "w1", TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();

        if (activity != null)
        {
            host.SubmitActivitySuccess(activity.Token, "Hi Sujith");
        }
    }

    return Results.Ok();
})
.WithName("SendEmailWorkflow");

app.MapPost("/server-down-workflow/{data}", (IWorkflowHost host, IServiceProvider serviceProvider, string data) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<ServerDownDto>)).Cast<IWorkflow<ServerDownDto>>();

    foreach (var workflow in workflows)
    {
        host.StartWorkflow(workflow.Id, workflow.Version, new ServerDownDto { Input = data });
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");
    }

    return Results.Ok();
})
.WithName("SeverDownWorkflow");

app.MapPost("/invitation-workflow/{rsvp}", (IWorkflowHost host, IServiceProvider serviceProvider, string rsvp) =>
{
    var workflows = serviceProvider.GetServices(typeof(IWorkflow<PartyRsvpDto>)).Cast<IWorkflow<PartyRsvpDto>>();

    foreach (var workflow in workflows)
    {
        host.StartWorkflow(workflow.Id, workflow.Version, new PartyRsvpDto { Input = rsvp });
        Console.WriteLine($"Workflow({workflow.Id}-{workflow.Version}) started!");
    }

    return Results.Ok();
})
.WithName("InvitationWorkflow");

app.MapPost("/stop-workflow", (IWorkflowHost host) =>
{
    Console.WriteLine("Workflow host stopped!");
    host.Stop();
    return Results.Ok();
})
.WithName("StopWorkflow");

app.Run();