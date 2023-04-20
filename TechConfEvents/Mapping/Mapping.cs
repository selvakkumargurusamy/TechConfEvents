using TechConfEvents.Dto;
using TechConfEvents.Models;

namespace TechConfEvents.Mapping
{
    public static class Mapping
    {
        public static EventDto Map(this Event @event)
        {
            EventDto dto = new EventDto();
            dto.EventId = @event.EventId;
            dto.EventTitle = @event.EventTitle;
            dto.Description = @event.Description;
            dto.EventStartDate = @event.EventStartDate;
            dto.EventEndDate = @event.EventEndDate;
            dto.IsOnline = @event.IsOnline;
            dto.LinkForDetails = @event.LinkForDetails;
            dto.Website = @event.Website;
            dto.Venue = @event.Venue;
            dto.SpeakerId = @event.SpeakerId;
            return dto;
        }

        public static EventsDto MapEvents(this Event @event)
        {
            EventsDto dto = new EventsDto();
            dto.EventId = @event.EventId;
            dto.EventTitle = @event.EventTitle;
            dto.Description = @event.Description;
            dto.EventStartDate = @event.EventStartDate;
            dto.EventEndDate = @event.EventEndDate;
            dto.IsOnline = @event.IsOnline;
            dto.LinkForDetails = @event.LinkForDetails;
            dto.Website = @event.Website;
            dto.Venue = @event.Venue;
            dto.SpeakerId = @event.SpeakerId;
            dto.SpeakerName = @event.Speaker.Name;
            return dto;
        }

        public static IEnumerable<EventsDto> Map(this IEnumerable<Event> @events)
        {
            List<EventsDto> eventDtos = new List<EventsDto>();
            foreach (var item in @events)
            {
                eventDtos.Add(MapEvents(item));
            }
            
            return eventDtos;
        }

        public static SpeakerDto Map(this Speaker speaker)
        {
            SpeakerDto dto = new SpeakerDto();
            dto.SpeakerId = speaker.SpeakerId;
            dto.Name = speaker.Name;
            dto.Biography = speaker.Biography;
            dto.SocialLinks = speaker.SocialLinks;
            return dto;
        }

        public static IEnumerable<SpeakerDto> Map(this IEnumerable<Speaker> speakers)
        {
            List<SpeakerDto> speakersDtos = new List<SpeakerDto>();
            foreach (var item in speakers)
            {
                speakersDtos.Add(Map(item));
            }

            return speakersDtos;
        }       
    }
}
