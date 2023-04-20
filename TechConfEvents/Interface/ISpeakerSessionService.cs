using TechConfEvents.Dto;
using TechConfEvents.Models;

namespace TechConfEvents.Interface
{
    public interface ISpeakerSessionService
    {
        IEnumerable<EventSpeakerDto> GetAllEventSpeakerSession();
        IEnumerable<EventSpeakerDto> GetEventSpeakerSession(Guid eventId);
        bool AddSpeakerSession(SpeakerSessionDto speakerSessionDto);
        bool UpdateSpeakerSession(SpeakerSessionDto speakerSessionDto);
        bool DeleteSpeakerSession(Guid speakerSessionId);

        bool DeleteSpeakerSessionByEventId(Guid eventId);
    }
}
