using Workflow.Events.Dto;

using WorkflowCore.Interface;

namespace Workflow.Events
{
    public class Alarm : IWorkflow<AlarmDto>
    {
        public string Id => "Alarm";

        public int Version => 1;

        public void Build(IWorkflowBuilder<AlarmDto> builder)
        {
            builder
                .StartWith<AlarmStep>()
                    .Input(step => step.Input, data => data.Input)
                    .Output(data => data.Output, step => step.Input)
                .WaitFor(AlarmEvent.Name, data => AlarmEvent.Id)
                    .Output(data => data.EventOutput, evnt => evnt.EventData)
                .Then<WakeUpStep>()
                    .Input(step => step.Input, data => $"{data.Output} {data.EventOutput}");
        }
    }
}
