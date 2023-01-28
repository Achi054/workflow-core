using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Greeting.Steps
{
    public class LogStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Input);

            return ExecutionResult.Next();
        }
    }
}
