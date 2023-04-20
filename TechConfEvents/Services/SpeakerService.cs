using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;

namespace TechConfEvents.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository speakerRepository;
        public SpeakerService(ISpeakerRepository _speakerRepository) {
            speakerRepository = _speakerRepository;
        }

        public IEnumerable<Speaker> GetAllSpeakers()
        {
            return speakerRepository.GetAllSpeakers();
        }


        public Guid AddSpeaker(SpeakerDto speakerDto)
        {
            return speakerRepository.AddSpeaker(speakerDto);
        }

        public bool DeleteSpeaker(Guid speakerId)
        {
            return speakerRepository.DeleteSpeaker(speakerId);
        }

      
        public Speaker GetSpeaker(Guid speakerId)
        {
            return speakerRepository.GetSpeaker(speakerId);
        }

        public bool UpdateSpeaker(SpeakerDto speakerDto)
        {
            return speakerRepository.UpdateSpeaker(speakerDto);
        }
    }
}
