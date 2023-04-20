namespace TechConfEvents.Dto
{
    public class SpeakerDto
    {
        public Guid? SpeakerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Biography { get; set; }

        public string? SocialLinks { get; set; }


    }
}
