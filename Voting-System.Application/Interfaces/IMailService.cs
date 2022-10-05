using Voting_System.Application.Models.MailDto;

namespace Voting_System.Application.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
