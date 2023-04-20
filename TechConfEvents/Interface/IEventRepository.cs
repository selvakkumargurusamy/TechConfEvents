using TechConfEvents.Dto;
using TechConfEvents.Models;

namespace TechConfEvents.Interface
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        Guid AddEvent(EventDto eventDto);
        Event GetEvent(Guid eventId);
        bool UpdateEvent(EventDto eventDto);
        bool DeleteEvent(Guid eventId);

        bool UpdateEventSpeakar(Guid eventId, Guid speakarId);
    }
}
