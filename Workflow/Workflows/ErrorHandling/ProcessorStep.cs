using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.ErrorHandling
{
    public class ProcessorStep : StepBody
    {
        public string Input { get; set; } = default!;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if(string.IsNullOrWhiteSpace(Input))
                throw new InvalidOperationException("Server Down!");
            
            Console.WriteLine($"Process {Input} recieved!");

            return ExecutionResult.Next();
        }
    }
}
