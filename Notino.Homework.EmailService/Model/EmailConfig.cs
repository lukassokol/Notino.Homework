namespace Notino.Homework.EmailService.Model;

public class EmailConfig
{
    public string From { get; set; }
    public string SMTPServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
