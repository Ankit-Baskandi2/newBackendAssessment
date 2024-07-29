using AssessementProjectForAddingUser.Application.Interface.IServices;
using System.Net.Mail;
using System.Net;


namespace AssessementProjectForAddingUser.Infrastructure.ImplementingInterface.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var mail = "ankitbaskandi@gmail.com";
                var pw = "nnix oveo nwmo rrkm";

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, pw)
                };

                return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, body));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
