using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Greeting.Steps
{
    public class ClosureStep : StepBody
    {
        public string Input { get; set; } = default!;

        public string Output { get; private set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Recieved {Input}");

            Output = $"{Input} Thank you!";

            return ExecutionResult.Next();
        }
    }
}
