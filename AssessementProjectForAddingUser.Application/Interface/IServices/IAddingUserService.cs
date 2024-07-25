using AssessementProjectForAddingUser.Domain.DTOs;

namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IAddingUserService
    {
        Task<ResponseDto> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos);
        Task<bool> LoginCredentialChecking(LoginCredentialDto loginCredential);
        Task<ResponseDto> GetAllUsers();
        Task<ResponseDto> DeleteUserDetail(int Id);
        Task<ResponseDto> UpdateUserDetail(UserDetailsAnkitDtos detailsAnkitDtos);
        Task<ResponseDto> SendEmailToForgotPassword(string email);
        
    }
}
