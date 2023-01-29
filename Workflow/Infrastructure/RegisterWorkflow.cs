using Workflow.Events;
using Workflow.Events.Dto;
using Workflow.Greeting;
using Workflow.Greeting.Dto;
using Workflow.Workflows.Activities;
using Workflow.Workflows.DecisionBranch;
using Workflow.Workflows.ErrorHandling;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Infrastructure
{
    public static class RegisterWorkflow
    {
        public static IApplicationBuilder UseWorkflow(this IApplicationBuilder appBuilder)
        {
            IWorkflowHost host = appBuilder.ApplicationServices.GetRequiredService<IWorkflowHost>();

            host.RegisterWorkflow<Hello, HelloDto>();
            host.RegisterWorkflow<Alarm, AlarmDto>();
            host.RegisterWorkflow<SendEmail, SendEmailDto>();
            host.RegisterWorkflow<ServerDown, ServerDownDto>();
            host.RegisterWorkflow<PartyRsvp, PartyRsvpDto>();

            host.Start();
            return appBuilder;
        }

        public static IServiceCollection AddSteps(this IServiceCollection services)
        {
            var steps = typeof(RegisterWorkflow).Assembly.GetTypes()
                .Where(x => typeof(StepBody).IsAssignableFrom(x));

            foreach (var step in steps)
            {
                services.AddScoped(step);
            }

            return services;
        }

        public static IServiceCollection AddWorkflows(this IServiceCollection services)
        {
            services.AddWorkflow();

            services.AddScoped<IWorkflow<HelloDto>, Hello>();
            services.AddScoped<IWorkflow<AlarmDto>, Alarm>();
            services.AddScoped<IWorkflow<SendEmailDto>, SendEmail>();
            services.AddScoped<IWorkflow<ServerDownDto>, ServerDown>();
            services.AddScoped<IWorkflow<PartyRsvpDto>, PartyRsvp>();

            return services;
        }
    }
}
