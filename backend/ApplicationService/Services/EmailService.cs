using MailKit.Net.Smtp;
using MimeKit;

namespace MC426_Backend.ApplicationService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) => _configuration = configuration;

        public bool SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:Sender"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:SenderPassword"]);

            try
            {
                smtp.Send(message);
            }
            catch
            {
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }

            return true;
        }
    }
}