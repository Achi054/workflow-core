using WorkflowCore.Interface;

namespace Workflow.Infrastructure
{
    public class WorkflowMiddleware : IMiddleware
    {
        private readonly IWorkflowHost _host;

        public WorkflowMiddleware(IWorkflowHost host)
        {
            _host = host;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _host.Start();
            Console.WriteLine("Workflow started!");

            return next(context);
        }
    }
}
