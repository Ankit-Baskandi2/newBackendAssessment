using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IAddingUserService
    {
        Task<string> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos);
        Task<bool> LoginCredentialChecking(LoginCredentialDto loginCredential);
        Task<IEnumerable<UserDetailsAnkit>> GetAllUsers();
        Task<string> DeleteUserDetail(int Id);
        Task<string> SendEmailToForgotPassword(string email);
    }
}
