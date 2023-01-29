using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Events
{
    public class WakeUpStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{Input}, Wake up!");

            return ExecutionResult.Next();
        }
    }
}
