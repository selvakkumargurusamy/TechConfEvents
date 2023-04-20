using System.Data.Entity;
using System.Reflection.Metadata.Ecma335;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;

namespace TechConfEvents.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly TechConfContext _context;

        public EventRepository(TechConfContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            var lstEvents = _context.Events.ToList();

            foreach (var evt in lstEvents)
                _context.Entry(evt).Reference(x => x.Speaker).Load();

            return lstEvents;
        }

        public Event GetEvent(Guid eventId)
        {
            var objEvent = _context.Events.SingleOrDefault(e => e.EventId == eventId);

            if (objEvent == null) return new Event();

            _context.Entry(objEvent).Reference(s => s.Speaker).Load();
            //_context.Entry(objEvent).Collection(s => s.SpeakerSessions).Load(); // loads Courses collection 

            return objEvent;
        }


        public Guid AddEvent(EventDto eventDto)
        {
            try
            {
                if (eventDto == null) return Guid.Empty;

                Event objEvent = new Event();
                objEvent.EventId = Guid.NewGuid();
                objEvent.EventTitle = eventDto.EventTitle;
                objEvent.Description = eventDto.Description;
                objEvent.EventStartDate = eventDto.EventStartDate;
                objEvent.EventEndDate = eventDto.EventEndDate;
                objEvent.LinkForDetails = eventDto.LinkForDetails;
                objEvent.IsOnline = eventDto.IsOnline;
                objEvent.Website = eventDto.Website;
                objEvent.Venue = eventDto.Venue;
                objEvent.SpeakerId = (Guid)eventDto.SpeakerId;

                _context.Events.Add(objEvent);
                _context.SaveChanges();

                return objEvent.EventId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }

        }

        public bool DeleteEvent(Guid eventId)
        {
            try
            {
                var objEvent = _context.Events.FirstOrDefault(e => e.EventId == eventId);

                if (objEvent == null) return false;

                _context.Events.Remove(objEvent);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool UpdateEvent(EventDto eventDto)
        {
            try
            {
                if (eventDto == null || eventDto.EventId == Guid.Empty || eventDto.EventId == null) return false;

                var objEvent = _context.Events.SingleOrDefault(b => b.EventId == eventDto.EventId);

                if (objEvent == null) return false;

                objEvent.EventTitle = eventDto.EventTitle;
                objEvent.Description = eventDto.Description;
                objEvent.EventStartDate = eventDto.EventStartDate;
                objEvent.EventEndDate = eventDto.EventEndDate;
                objEvent.LinkForDetails = eventDto.LinkForDetails;
                objEvent.IsOnline = eventDto.IsOnline;
                objEvent.Website = eventDto.Website;
                objEvent.Venue = eventDto.Venue;
                objEvent.SpeakerId = eventDto.SpeakerId;

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateEventSpeakar(Guid eventId, Guid speakarId)
        {
            try
            {
                if (eventId == Guid.Empty || speakarId == Guid.Empty) return false;

                var objEvent = _context.Events.SingleOrDefault(b => b.EventId == eventId);

                if (objEvent == null) return false;

                objEvent.SpeakerId = speakarId;

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
