using System.Data.Entity;
using System.Reflection.Metadata.Ecma335;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;

namespace TechConfEvents.Repository
{
    public class SpeakerSessionRepository : ISpeakerSessionRepository
    {
        private readonly TechConfContext _context;

        public SpeakerSessionRepository(TechConfContext context)
        {
            _context = context;
        }

        public IEnumerable<EventSpeakerDto> GetAllEventSpeakerSession()
        {
            List<EventSpeakerDto> eventSpeakerDtos = new List<EventSpeakerDto>();

            var speakerSessions = _context.SpeakerSessions.ToList();

            foreach (var item in speakerSessions)
            {
                _context.Entry(item).Reference(x => x.Event).Load();
                _context.Entry(item.Event).Reference(x => x.Speaker).Load();

                EventSpeakerDto eventSpeakerDto = new EventSpeakerDto();
                eventSpeakerDto.EventTitle = item.Event.EventTitle;
                eventSpeakerDto.EventDescription = item.Event.Description;
                eventSpeakerDto.SpeakerName = item.Event.Speaker.Name;
                eventSpeakerDto.SpeakerSession = item.SessionDate;
                eventSpeakerDtos.Add(eventSpeakerDto);
            }

            return eventSpeakerDtos;
        }

        public IEnumerable<EventSpeakerDto> GetEventSpeakerSession(Guid eventId)
        {
            List<EventSpeakerDto> eventSpeakerDtos = new List<EventSpeakerDto>();

            var speakerSessions = _context.SpeakerSessions.Where(e => e.EventId == eventId);

            foreach (var item in speakerSessions)
            {
                _context.Entry(item).Reference(x => x.Event).Load();
                _context.Entry(item).Reference(x => x.Event.Speaker).Load();

                EventSpeakerDto eventSpeakerDto = new EventSpeakerDto();
                eventSpeakerDto.EventTitle = item.Event.EventTitle;
                eventSpeakerDto.EventDescription = item.Event.Description;
                eventSpeakerDto.SpeakerName = item.Event.Speaker.Name;
                eventSpeakerDto.SpeakerSession = item.SessionDate;
                eventSpeakerDtos.Add(eventSpeakerDto);
            }

            return eventSpeakerDtos;
        }

        public bool AddSpeakerSession(SpeakerSessionDto speakerSessionDto)
        {
            try
            {
                if (speakerSessionDto == null) return false;

                SpeakerSession speakerSession = new SpeakerSession();
                speakerSession.SpeakerSessionId = Guid.NewGuid();
                speakerSession.EventId = speakerSessionDto.EventId;
                speakerSession.SessionDate = speakerSessionDto.SessionDate;

                _context.SpeakerSessions.Add(speakerSession);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSpeakerSession(SpeakerSessionDto speakerSessionDto)
        {
            try
            {
                if (speakerSessionDto == null || speakerSessionDto.EventId == Guid.Empty) return false;

                var speakerSession = _context.SpeakerSessions.FirstOrDefault(e => e.SpeakerSessionId == speakerSessionDto.SpeakerSessionId);

                if (speakerSession == null) return false;

                //speakerSession.EventId = speakerSessionDto.EventId;
                speakerSession.SessionDate = speakerSessionDto.SessionDate;

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSpeakerSession(Guid speakerSessionId)
        {

            try
            {
                var speakerSession = _context.SpeakerSessions.FirstOrDefault(e => e.SpeakerSessionId == speakerSessionId);

                if (speakerSession == null) return false;

                _context.SpeakerSessions.Remove(speakerSession);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteSpeakerSessionByEventId(Guid eventId)
        {

            try
            {
                var speakerSession = _context.SpeakerSessions.Where(e => e.EventId == eventId);

                if (speakerSession == null || speakerSession?.Count() == 0) return false;

                _context.SpeakerSessions.RemoveRange(speakerSession);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
