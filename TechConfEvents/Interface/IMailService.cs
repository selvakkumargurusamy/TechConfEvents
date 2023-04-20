using TechConfEvents.Dto;

namespace TechConfEvents.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
    