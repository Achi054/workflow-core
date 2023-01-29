using Microsoft.AspNetCore.SignalR.Protocol;
using System.Xml.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.Activities
{
    public class SendEmail : IWorkflow<SendEmailDto>
    {
        public string Id => "SendEmail";

        public int Version => 1;

        public void Build(IWorkflowBuilder<SendEmailDto> builder)
        {
            builder
                .StartWith(ctx => ExecutionResult.Next())
                .Activity(SendEmailActivity.Name, (data) => data.Input)
                    .Output(data => data.Output, step => step.Result)
                .Then<SendEmailStep>()
                    .Input(step => step.Output, data => data.Output);
        }

    }
}
