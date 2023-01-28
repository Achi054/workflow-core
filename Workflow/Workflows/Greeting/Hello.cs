using Workflow.Greeting.Dto;
using Workflow.Greeting.Steps;

using WorkflowCore.Interface;

namespace Workflow.Greeting
{
    public class Hello : IWorkflow<HelloDto>
    {
        public string Id => "Hello";

        public int Version => 1;

        public void Build(IWorkflowBuilder<HelloDto> builder)
        {
            builder.StartWith<GreetingStep>()
                    .Input(step => step.Input, data => data.Input)
                    .Output(data => data.Output, step => step.Output)
                .Then<ClosureStep>()
                    .Input(step => step.Input, data => data.Output)
                    .Output(data => data.Output, step => step.Output)
                .Then<LogStep>()
                   .Input(step => step.Input, data => data.Output);
        }
    }
}
