using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Notino.Homework.EmailService.Interfaces;
using Notino.Homework.EmailService.Model;

namespace Notino.Homework.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailConfig _config;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(EmailConfig config, ILogger<EmailSender> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage email)
    {
        var mailMessage = CreateEmailMessage(email);
        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_config.SMTPServer, _config.Port, true);
            await client.AuthenticateAsync(_config.UserName, _config.Password);

            await client.SendAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }

    public MimeMessage CreateEmailMessage(EmailMessage email)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_config.From));
        mimeMessage.To.AddRange(email.To);
        mimeMessage.Subject = email.Subject;
        mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = string.Format("<h1>Email</h1>", email.Content)
        };

        return mimeMessage;
    }
}
