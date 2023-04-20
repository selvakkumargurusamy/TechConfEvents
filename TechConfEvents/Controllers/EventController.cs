using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Mapping;
using TechConfEvents.Models;
using TechConfEvents.Services;

namespace TechConfEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;
        private readonly IMailService _mailService;
        private readonly ISpeakerSessionService _speakerSessionService;

        public EventController(ILogger<EventController> logger, IEventService eventService, IMailService mailService, ISpeakerSessionService speakerSessionService)
        {
            _logger = logger;
            _eventService = eventService;
            _mailService = mailService;
            _speakerSessionService = speakerSessionService;
        }


        [HttpGet]
        [Route("GetEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EventDto>> GetEvents()
        {
            var events = _eventService.GetAllEvents();
            if (events == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(events.Map());
        }

        [HttpGet]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventDto> GetEvent(Guid eventId)
        {
            if (eventId == Guid.Empty)
            {
                return BadRequest("Invalid Event Id");
            }

            var eventObj = _eventService.GetEvent(eventId);
            if (eventObj == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(eventObj.Map());
        }

        [HttpPost]
        [Route("CreateEvent")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<EventDto> CreateEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (eventDto == null)
            {
                return BadRequest(eventDto);
            }

            if (eventDto.EventId != Guid.Empty && eventDto.EventId != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var eventId = _eventService.AddEvent(eventDto);

            if (eventId == Guid.Empty)
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Create a Event");

            _speakerSessionService.AddSpeakerSession(new SpeakerSessionDto() { SpeakerSessionId = Guid.NewGuid(), EventId = eventId, SessionDate = eventDto.EventStartDate });

            eventDto.EventId = eventId;

            return Ok(eventDto);
        }

        [HttpPut]
        [Route("UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<bool> UpdateEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (eventDto == null)
            {
                return BadRequest(eventDto);
            }

            if (eventDto.EventId == Guid.Empty)
            {
                return BadRequest(eventDto);
            }

            
            var isUpdated = _eventService.UpdateEvent(eventDto);

            if (!isUpdated)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> DeleteEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (eventId == Guid.Empty)
            {
                return BadRequest(eventId);
            }

           _speakerSessionService.DeleteSpeakerSessionByEventId(eventId);

            var isDeleted = _eventService.DeleteEvent(eventId);

            if (!isDeleted)
                return StatusCode(StatusCodes.Status500InternalServerError);
            

            return Ok(isDeleted);
        }

        [HttpPut]
        [Route("{eventId}/UpdateEventSpeakar")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<bool>> UpdateEventSpeakar(Guid eventId, [FromBody] Guid speakarId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (eventId == Guid.Empty || speakarId == Guid.Empty)
            {
                return BadRequest(eventId);
            }


            var isUpdated = _eventService.UpdateEventSpeakar(eventId, speakarId);

            if (!isUpdated)
                return StatusCode(StatusCodes.Status500InternalServerError);

            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = "abc@gmail.com";
            mailRequest.Subject = "Test";
            mailRequest.Body = "Assigned to the Event " + _eventService.GetEvent(eventId).EventTitle;
            await _mailService.SendEmailAsync(mailRequest);

            return Ok(isUpdated);
        }

    }
}