using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
