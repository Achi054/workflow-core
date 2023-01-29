using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.DecisionBranch
{
    public class AcceptedStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Invite {Input}!");

            return ExecutionResult.Next();
        }
    }
}
