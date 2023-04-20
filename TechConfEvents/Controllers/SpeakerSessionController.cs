using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Mapping;
using TechConfEvents.Services;

namespace TechConfEvents.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class SpeakerSessionController : ControllerBase
    {
        private readonly ILogger<SpeakerSessionController> _logger;
        private readonly ISpeakerSessionService _speakerSessionService;
        private readonly IMailService _mailService;
        private readonly ICSVService _csvService;
        private readonly IEventService _eventService;

        public SpeakerSessionController(ILogger<SpeakerSessionController> logger,
            ISpeakerSessionService speakerSessionService,
            IMailService mailService,
            ICSVService csvService,
            IEventService eventService
            )
        {
            _logger = logger;
            _speakerSessionService = speakerSessionService;
            _mailService = mailService;
            _csvService = csvService;
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetAllEventSpeakerSession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EventSpeakerDto>> GetEventSpeakerSession()
        {
            var speakerSession = _speakerSessionService.GetAllEventSpeakerSession();
            if (speakerSession == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(speakerSession);
        }

        [HttpGet]
        [Route("GetSpeakerSession/{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EventSpeakerDto>> GetEventSpeakerSession(Guid eventId)
        {
            var speakerSession = _speakerSessionService.GetEventSpeakerSession(eventId);
            if (speakerSession == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(speakerSession);
        }


        [HttpPost]
        [Route("CreateSpeakerSession")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<bool>> CreateSpeakerSession([FromBody] SpeakerSessionDto speakerSessionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerSessionDto == null)
            {
                return BadRequest(speakerSessionDto);
            }

            var objEvent = _eventService.GetEvent(speakerSessionDto.EventId);

            if (objEvent == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "EventId is not Found");
           

            if (!(speakerSessionDto.SessionDate >= objEvent.EventStartDate && speakerSessionDto.SessionDate <= objEvent.EventEndDate))
                return StatusCode(StatusCodes.Status500InternalServerError, "SessionDate is not mentioned in Event StartDate and Event EndDate");

            bool isAdded = _speakerSessionService.AddSpeakerSession(speakerSessionDto);

            if (!isAdded)
                return StatusCode(StatusCodes.Status500InternalServerError);

            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = "abc@gmail.com";
            mailRequest.Subject = "Test";
            mailRequest.Body = "Session Confirmed at " + speakerSessionDto.SessionDate.ToShortDateString();
            await _mailService.SendEmailAsync(mailRequest);

            return Ok(isAdded);
        }

        [HttpPost]
        [Route("UpdateSpeakerSession")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateSpeakerSession([FromBody] SpeakerSessionDto speakerSessionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerSessionDto == null)
            {
                return BadRequest(speakerSessionDto);
            }

            var objEvent = _eventService.GetEvent(speakerSessionDto.EventId);

            if (objEvent == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "EventId is not Found");
          
            if (!(speakerSessionDto.SessionDate >= objEvent.EventStartDate && speakerSessionDto.SessionDate <= objEvent.EventEndDate))
                return StatusCode(StatusCodes.Status500InternalServerError, "SessionDate is not mentioned in Event StartDate and Event EndDate");

            var isUpdated = _speakerSessionService.UpdateSpeakerSession(speakerSessionDto);

            if (!isUpdated)
                return StatusCode(StatusCodes.Status500InternalServerError);

            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = "abc@gmail.com";
            mailRequest.Subject = "Test";
            mailRequest.Body = "Session Confirmed at " + speakerSessionDto.SessionDate.ToShortDateString();
            await _mailService.SendEmailAsync(mailRequest);

            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("{speakerSessionId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> DeleteSpeakerSession(Guid speakerSessionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerSessionId == Guid.Empty)
            {
                return BadRequest(speakerSessionId);
            }

            var isDeleted = _speakerSessionService.DeleteSpeakerSession(speakerSessionId);

            if (!isDeleted)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(isDeleted);
        }

        [HttpPost]
        [Route("Export")]
        public IActionResult ExportEventsCSV()
        {
            var eventSpeakers = _speakerSessionService.GetAllEventSpeakerSession().ToList();
            _csvService.WriteCSV<EventSpeakerDto>(eventSpeakers);

            return Ok();
        }
    }
}