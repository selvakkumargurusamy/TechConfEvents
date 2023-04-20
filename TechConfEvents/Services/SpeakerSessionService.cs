using Microsoft.Extensions.Logging;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;
using TechConfEvents.Repository;

namespace TechConfEvents.Services
{
    public class SpeakerSessionService : ISpeakerSessionService
    {
        private readonly ISpeakerSessionRepository speakerSessionRepository;
        public SpeakerSessionService(ISpeakerSessionRepository _speakerSessionRepository) {
            speakerSessionRepository = _speakerSessionRepository;
        }

        public IEnumerable<EventSpeakerDto> GetAllEventSpeakerSession()
        {
            return speakerSessionRepository.GetAllEventSpeakerSession();
        }

        public IEnumerable<EventSpeakerDto> GetEventSpeakerSession(Guid eventId)
        {
            return speakerSessionRepository.GetEventSpeakerSession(eventId);
        }

        public bool AddSpeakerSession(SpeakerSessionDto speakerSessionDto)
        {
            return speakerSessionRepository.AddSpeakerSession(speakerSessionDto);
        }

        public bool UpdateSpeakerSession(SpeakerSessionDto speakerSessionDto)
        {
            return speakerSessionRepository.UpdateSpeakerSession(speakerSessionDto);
        }

        public bool DeleteSpeakerSession(Guid speakerSessionId)
        {
            return speakerSessionRepository.DeleteSpeakerSession(speakerSessionId);
        }

        public bool DeleteSpeakerSessionByEventId(Guid eventId)
        {
            return speakerSessionRepository.DeleteSpeakerSessionByEventId(eventId);
        }
    }
}
