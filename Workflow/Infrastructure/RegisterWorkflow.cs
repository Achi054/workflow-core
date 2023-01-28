using Workflow.Greeting;
using Workflow.Greeting.Dto;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Infrastructure
{
    public static class RegisterWorkflow
    {
        public static IApplicationBuilder UseWorkflow(this IApplicationBuilder ab)
        {
            IWorkflowHost host = ab.ApplicationServices.GetRequiredService<IWorkflowHost>();

            host.RegisterWorkflow<Hello, HelloDto>();

            return ab.UseMiddleware<WorkflowMiddleware>();
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

            return services;
        }
    }
}
