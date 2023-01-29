using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.DecisionBranch
{
    public class PartyRsvp : IWorkflow<PartyRsvpDto>
    {
        public string Id => "PartyRsvp";

        public int Version => 1;

        public void Build(IWorkflowBuilder<PartyRsvpDto> builder)
        {
            builder
                .CreateAcceptedBranch()
                .CreateRejectedBranch()
                .StartWith<InvitationStep>()
                    .Input(step => step.Input, data => data.Input)
                    .Output(data => data.Input, step => step.Input)
                .Decide(data => data.Input)
                    .Branch((data, _) => data.Input.ToLower() == "accepted", AcceptedBranch.WorkFlowSetup)
                    .Branch((data, _) => data.Input.ToLower() == "rejected", RejectedBranch.WorkFlowSetup);
        }
    }
}
