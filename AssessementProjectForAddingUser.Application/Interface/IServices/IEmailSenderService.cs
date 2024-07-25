
namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
