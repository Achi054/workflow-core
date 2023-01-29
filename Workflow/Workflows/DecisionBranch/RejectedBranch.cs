using WorkflowCore.Interface;

namespace Workflow.Workflows.DecisionBranch
{
    public static class RejectedBranch
    {
        public static IStepBuilder<PartyRsvpDto, RejectedStep> WorkFlowSetup { get; private set; } = default!;

        public static IWorkflowBuilder<PartyRsvpDto> CreateRejectedBranch(this IWorkflowBuilder<PartyRsvpDto> builder)
        {
            WorkFlowSetup = builder.CreateBranch()
                                   .StartWith<RejectedStep>()
                                        .Input(step => step.Input, data => data.Input);

            return builder;
        }
    }
}
