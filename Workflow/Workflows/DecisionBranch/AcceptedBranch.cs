using WorkflowCore.Interface;

namespace Workflow.Workflows.DecisionBranch
{
    public static class AcceptedBranch
    {
        public static IStepBuilder<PartyRsvpDto, AcceptedStep> WorkFlowSetup { get; private set; } = default!;

        public static IWorkflowBuilder<PartyRsvpDto> CreateAcceptedBranch(this IWorkflowBuilder<PartyRsvpDto> builder)
        {
            WorkFlowSetup = builder.CreateBranch()
                                   .StartWith<AcceptedStep>()
                                        .Input(step => step.Input, data => data.Input);

            return builder;
        }
    }
}
