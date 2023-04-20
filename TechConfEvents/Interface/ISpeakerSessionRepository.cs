using TechConfEvents.Dto;
using TechConfEvents.Models;

namespace TechConfEvents.Interface
{
    public interface ISpeakerSessionRepository
    {
        IEnumerable<EventSpeakerDto> GetAllEventSpeakerSession();
        IEnumerable<EventSpeakerDto> GetEventSpeakerSession(Guid eventId);
        bool AddSpeakerSession(SpeakerSessionDto speakerSessionDto);
        bool UpdateSpeakerSession(SpeakerSessionDto speakerDto);
        bool DeleteSpeakerSession(Guid speakerId);
        bool DeleteSpeakerSessionByEventId(Guid eventId);
    }
}
