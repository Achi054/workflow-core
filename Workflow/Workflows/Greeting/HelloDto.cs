namespace Workflow.Greeting.Dto
{
    public class HelloDto
    {
        public string Input { get; set; } = default!;

        public string Output { get; private set; } = default!;
    }
}
