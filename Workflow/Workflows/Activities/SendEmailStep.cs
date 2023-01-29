using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.Activities
{
    public class SendEmailStep : StepBody
    {
        public string Output { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{Output}, Email sent!");
            return ExecutionResult.Next();
        }
    }
}
