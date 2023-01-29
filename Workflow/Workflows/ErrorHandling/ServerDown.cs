using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.ErrorHandling
{
    public class ServerDown : IWorkflow<ServerDownDto>
    {
        public string Id => "ServerDown";

        public int Version => 1;

        public void Build(IWorkflowBuilder<ServerDownDto> builder)
        {
            builder
                .StartWith<ProcessorStep>()
                    .Input(step => step.Input, data => data.Input)
                    .Output(data => data.Output, step => step.Input)
                    .OnError(WorkflowErrorHandling.Retry, TimeSpan.FromSeconds(5))
                .Then<ClosureStep>()
                    .Input(step => step.Input, data => data.Output);
        }
    }
}
