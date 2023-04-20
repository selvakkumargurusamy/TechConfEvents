using System.Reflection.Metadata.Ecma335;
using TechConfEvents.Dto;
using TechConfEvents.Interface;
using TechConfEvents.Models;

namespace TechConfEvents.Repository
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly TechConfContext _context;

        public SpeakerRepository(TechConfContext context)
        {
            _context = context;
        }

        public IEnumerable<Speaker> GetAllSpeakers()
        {
            return _context.Speakers.ToList();
        }

        public Guid AddSpeaker(SpeakerDto speakerDto)
        {
            try
            {
                if (speakerDto == null) return Guid.Empty;

                Speaker speaker = new Speaker();
                speaker.SpeakerId = Guid.NewGuid();
                speaker.Name = speakerDto.Name;
                speaker.Biography = speakerDto.Biography;
                speaker.SocialLinks = speakerDto.SocialLinks;

                _context.Speakers.Add(speaker);
                _context.SaveChanges();

                return speaker.SpeakerId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }

        }

        public Speaker GetSpeaker(Guid speakerId)
        {
            var speaker = _context.Speakers.FirstOrDefault(e => e.SpeakerId == speakerId);
            return speaker;
        }

        public bool UpdateSpeaker(SpeakerDto speakerDto)
        {
            try
            {
                if (speakerDto == null || speakerDto.SpeakerId == Guid.Empty || speakerDto.SpeakerId == null) return false;

                var speaker = _context.Speakers.SingleOrDefault(b => b.SpeakerId == speakerDto.SpeakerId);

                if(speaker ==null) return false;

                speaker.Name = speakerDto.Name;
                speaker.Biography = speakerDto.Biography;
                speaker.SocialLinks = speakerDto.SocialLinks;

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool DeleteSpeaker(Guid speakerId)
        {
            try
            {
                var speaker = _context.Speakers.FirstOrDefault(e => e.SpeakerId == speakerId);

                if (speaker == null) return false;

                _context.Speakers.Remove(speaker);
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
