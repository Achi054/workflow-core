namespace Workflow.Events.Dto
{
    public class AlarmDto
    {   
        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public string EventOutput { get; set; } = default!;
    }
}
