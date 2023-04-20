namespace TechConfEvents.Dto
{
    public class EventsDto
    {
        public Guid? EventId { get; set; }
        public string EventTitle { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        public bool IsOnline { get; set; }

        public string? Venue { get; set; }

        public string? Website { get; set; }

        public string? LinkForDetails { get; set; }

        public Guid SpeakerId { get; set; }

        public string SpeakerName { get; set; }

    }
}
