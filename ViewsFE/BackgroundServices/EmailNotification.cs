using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ViewsFE.BackgroundServices
{
    public class EmailNotification
    {
        private readonly string _emailFrom;
        private readonly string _emailPassword;
        private readonly string _smtpServer;
        private readonly int _smtpPort;

        public EmailNotification(IConfiguration configuration)
        {
            _emailFrom = configuration["EmailSettings:EmailFrom"];
            _emailPassword = configuration["EmailSettings:EmailPassword"];
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HangKenh", _emailFrom));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailFrom, _emailPassword);
                    await client.SendAsync(emailMessage);
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                catch (AuthenticationException ex)
                {
                    Console.WriteLine($"Authentication error: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
