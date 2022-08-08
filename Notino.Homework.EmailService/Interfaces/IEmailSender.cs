using Notino.Homework.EmailService.Model;

namespace Notino.Homework.EmailService.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage email);
}
