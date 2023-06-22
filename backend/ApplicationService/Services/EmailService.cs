using MailKit.Net.Smtp;
using MimeKit;

namespace MC426_Backend.ApplicationService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:Sender"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, false);
            await smtp.AuthenticateAsync(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:SenderPassword"]);

            try
            {
                await smtp.SendAsync(message);
            }
            catch
            {
                Console.Error.WriteLine($"Não foi possível enviar o email para {email}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}