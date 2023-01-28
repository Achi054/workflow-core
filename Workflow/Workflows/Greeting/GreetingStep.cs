using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Greeting.Steps
{
    public class GreetingStep : StepBody
    {
        public string Input { get; set; } = default!;

        public string Output { get; private set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Recieved {Input}");

            Output = $"{Input} Good morning.";

            return ExecutionResult.Next();
        }
    }
}
