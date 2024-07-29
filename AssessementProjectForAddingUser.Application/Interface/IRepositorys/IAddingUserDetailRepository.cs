using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;

namespace AssessementProjectForAddingUser.Application.Interface.IRepositorys
{
    public interface IAddingUserDetailRepository
    {
        Task<ResponseDto> AddingUserInDb(UserDetailsAnkit userAddress);

        Task<ResponseDto> LoginCredentialChecking(LoginCredentials loginCredential);

        Task<ResponseDto> GetAllUsers();

        Task<ResponseDto> DeleteUserDetail(long Id);

        Task<ResponseDto> UpdateUserDetail(UserDetailsAnkitDtos userDetailsAnkit);

        Task<bool> EmailIsPresentOrNot(string email);

        Task<UserDetailsAnkit> GetUserByEmail(string email);

        Task<UserDetailsAnkit> GetUserById(int id);
        
        Task<ResponseDto> UpdatePassword(int Id, string password);
        //Task<ResponseDto> ChangePassword(string oldPassword,  string newPassword);
    }
}
