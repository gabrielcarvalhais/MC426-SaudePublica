using MailKit.Net.Smtp;
using MimeKit;

namespace MC426_Backend.ApplicationService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public static string EMAIL_HEADER = @"
            <head>
                <style>
                    body {
                    font-family: Arial, sans-serif;
                    background-color: #f5f5f5;
                    }
                    
                    .container {
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #ffffff;
                    border: 1px solid #cccccc;
                    }
                    
                    .header {
                    text-align: center;
                    margin-bottom: 30px;
                    }
                    
                    .title {
                    color: #333333;
                    font-size: 24px;
                    margin-bottom: 10px;
                    }
                    
                    .subtitle {
                    color: #777777;
                    font-size: 18px;
                    margin-bottom: 20px;
                    }
                    
                    .appointment {
                    background-color: #f9f9f9;
                    border-radius: 5px;
                    padding: 10px;
                    margin-bottom: 20px;
                    }
                    
                    .appointment-title {
                    color: #333333;
                    font-size: 20px;
                    margin-bottom: 5px;
                    }
                    
                    .appointment-details {
                    color: #777777;
                    font-size: 16px;
                    }
                    
                    .footer {
                    text-align: center;
                    margin-top: 30px;
                    color: #777777;
                    font-size: 14px;
                    }
                </style>
            </head>
        ";

        public static string EMAIL_FOOTER = @"
            <div class=""footer"">
                <p>Atenciosamente,</p>
                <p>A equipe Saúde+Barão</p>
                </div>
            </div>
        ";

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