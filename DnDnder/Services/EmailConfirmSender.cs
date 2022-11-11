using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Tavern.Services
{
    public class EmailConfirmSender : IEmailSender
    {
        private const string EMAIL_SENDER_ACCOUNT = "TavernConfirmation@gmail.com";
        private const string EMAIL_SENDER_APP_PW = "gpzczrtyqjxpciky";

        public async Task SendEmailAsync(string UserEmail, string subject, string message)
        {
            MailMessage confirmMessage = new MailMessage();
            confirmMessage.From = new MailAddress(EMAIL_SENDER_ACCOUNT);
            confirmMessage.Subject = "Tavern -- " + subject;
            confirmMessage.To.Add(UserEmail);
            confirmMessage.Body = "<html><body>" + message + " </body></html>";
            confirmMessage.IsBodyHtml = true;

            var SmtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(EMAIL_SENDER_ACCOUNT, EMAIL_SENDER_APP_PW),
                EnableSsl = true
            };
            SmtpClient.Send(confirmMessage);
        }
    }
}
