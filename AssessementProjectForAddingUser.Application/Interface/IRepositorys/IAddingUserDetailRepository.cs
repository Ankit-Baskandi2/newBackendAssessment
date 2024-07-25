using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Application.Interface.IRepositorys
{
    public interface IAddingUserDetailRepository
    {
        Task<string> AddingUserInDb(UserDetailsAnkit userAddress);
        Task<bool> LoginCredentialChecking(LoginCredentials loginCredential);
        Task<IEnumerable<UserDetailsAnkit>> GetAllUsers();
        Task<string> DeleteUserDetail(int Id);
        //Task<string> UpdateUserDetail(UserDetailsAnkit userDetailsAnkit);
        Task<bool> EmailIsPresentOrNot(string email);
        Task<ResponseDto> ChangePassword(string oldPassword,  string newPassword);
    }
}
