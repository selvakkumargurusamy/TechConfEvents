using TechConfEvents.Dto;
using TechConfEvents.Models;

namespace TechConfEvents.Interface
{
    public interface ISpeakerRepository
    {
        IEnumerable<Speaker> GetAllSpeakers();
        Speaker GetSpeaker(Guid speakerId);
        Guid AddSpeaker(SpeakerDto speakerDto);       
        bool UpdateSpeaker(SpeakerDto speakerDto);
        bool DeleteSpeaker(Guid speakerId);
    }
}
