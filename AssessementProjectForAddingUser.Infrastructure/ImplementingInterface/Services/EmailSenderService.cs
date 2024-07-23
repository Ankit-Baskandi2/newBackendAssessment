using AssessementProjectForAddingUser.Application.Interface.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Infrastructure.ImplementingInterface.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string body)
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
    }
}
