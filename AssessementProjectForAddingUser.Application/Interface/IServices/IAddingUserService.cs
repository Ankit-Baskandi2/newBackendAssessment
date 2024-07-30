using AssessementProjectForAddingUser.Domain.DTOs;

namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IAddingUserService
    {
        Task<ResponseDto> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos);
        Task<ResponseDto> LoginCredentialChecking(LoginCredentialDto loginCredential);
        Task<ResponseDto> GetAllUsers();
        Task<ResponseDto> DeleteUserDetail(long id);
        Task<ResponseDto> UpdateUserDetail(UserDetailsAnkitDtos detailsAnkitDtos);
        Task<ResponseDto> SendEmailToForgotPassword(string email);
        Task<ResponseDto> ResetForgotedPasswod(ResetPasswordDto password, string token);

        Task<ResponseDto> ChangeLogedInUserPassword(ChangePasswordWhenLogedInDto changePassword, string token);
        
    }
}
