namespace TechConfEvents.Dto
{
    public class SpeakerSessionDto
    {
        public Guid? SpeakerSessionId { get; set; }

        public Guid EventId { get; set; }

        public DateTime SessionDate { get; set; }

    }
}
