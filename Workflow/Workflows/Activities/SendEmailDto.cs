using WorkflowCore.Interface;
using WorkflowCore.Primitives;

namespace Workflow.Workflows.Activities
{
    public class SendEmailDto
    {
        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;
    }
}
