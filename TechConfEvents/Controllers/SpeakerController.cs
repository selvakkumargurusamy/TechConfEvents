using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Mapping;

namespace TechConfEvents.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class SpeakerController : ControllerBase
    {
        private readonly ILogger<SpeakerController> _logger;
        private readonly ISpeakerService _speakerService;

        public SpeakerController(ILogger<SpeakerController> logger, ISpeakerService speakerService)
        {
            _logger = logger;
            _speakerService = speakerService;
        }

        [HttpGet]
        [Route("GetSpeakers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SpeakerDto>> GetSpeakers()
        {
            var speakers = _speakerService.GetAllSpeakers();
            if (speakers == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(speakers.Map());
        }

        [HttpGet]
        [Route("{speakerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SpeakerDto> Getspeaker(Guid speakerId)
        {
            if (speakerId == Guid.Empty)
            {
                return BadRequest("Invalid speaker Id");
            }

            var speakerObj = _speakerService.GetSpeaker(speakerId);
            if (speakerObj == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(speakerObj.Map());
        }


        [HttpPost]
        [Route("CreateSpeaker")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<SpeakerDto> CreateSpeaker([FromBody] SpeakerDto speakerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerDto == null)
            {
                return BadRequest(speakerDto);
            }

            if (speakerDto.SpeakerId != Guid.Empty && speakerDto.SpeakerId != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var speakerId = _speakerService.AddSpeaker(speakerDto);

            if(speakerId == Guid.Empty)
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Create a Speaker");

            speakerDto.SpeakerId = speakerId;

            return Ok(speakerDto);
        }

        [HttpPut]
        [Route("UpdateSpeaker")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> UpdateSpeaker([FromBody] SpeakerDto speakerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerDto == null)
            {
                return BadRequest(speakerDto);
            }

            if (speakerDto.SpeakerId == Guid.Empty)
            {
                return BadRequest(speakerDto);
            }

            var isUpdated = _speakerService.UpdateSpeaker(speakerDto);

            if (!isUpdated)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("{speakerId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> DeleteSpeaker(Guid speakerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (speakerId == Guid.Empty)
            {
                return BadRequest(speakerId);
            }

            var isDeleted = _speakerService.DeleteSpeaker(speakerId);

            if (!isDeleted)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(isDeleted);
        }
    }
}