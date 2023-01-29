using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.ErrorHandling
{
    public class ClosureStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{Input} process closed!");
            return ExecutionResult.Next();
        }
    }
}
