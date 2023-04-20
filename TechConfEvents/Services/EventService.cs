using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;
using TechConfEvents.Repository;

namespace TechConfEvents.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        public EventService(IEventRepository _eventRepository) {
            eventRepository = _eventRepository;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return eventRepository.GetAllEvents();
        }


        public Guid AddEvent(EventDto eventDto)
        {
            return eventRepository.AddEvent(eventDto);
        }

        public bool DeleteEvent(Guid eventId)
        {
            return eventRepository.DeleteEvent(eventId);
        }

        public Event GetEvent(Guid eventId)
        {
            return eventRepository.GetEvent(eventId);
        }

        public bool UpdateEvent(EventDto eventDto)
        {
            return eventRepository.UpdateEvent(eventDto);
        }

        public bool UpdateEventSpeakar(Guid eventId, Guid speakarId)
        {
            return eventRepository.UpdateEventSpeakar(eventId, speakarId);
        }
    }
}
