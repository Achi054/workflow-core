using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.DecisionBranch
{
    public class InvitationStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Invitation: {Input}");

            return ExecutionResult.Next();
        }
    }
}
