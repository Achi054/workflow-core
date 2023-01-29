using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Events
{
    public class AlarmStep : StepBody
    {
        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"{Input} received!");

            return ExecutionResult.Next();
        }
    }
}
