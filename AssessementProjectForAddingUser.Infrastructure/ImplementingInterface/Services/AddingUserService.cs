using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Application.Interface.IServices;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;

namespace AssessementProjectForAddingUser.Infrastructure.ImplementingInterface.Services
{
    public class AddingUserService : IAddingUserService
    {
        private readonly IAddingUserDetailRepository _repository;
        private readonly IEmailSenderService _emailSenderService;

        public AddingUserService(IAddingUserDetailRepository addingUserDetail, IEmailSenderService emailSenderService)
        {
            _repository = addingUserDetail;
            _emailSenderService = emailSenderService;
        }

        public async Task<ResponseDto> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos)
        {
            var message = "Use this email and passwod to login";
            var uniquePassword = GeneratePassword.GenerateUniquePassword();
            var credientailDetails = $"Email : {userDetailsAnkitDtos.Email}, Password :{uniquePassword}";
            _emailSenderService.SendEmailAsync(userDetailsAnkitDtos.Email, message, credientailDetails);


            var convertedfile = new UserDetailsAnkit
            {
                FirstName = userDetailsAnkitDtos.FirstName,
                MiddleName = userDetailsAnkitDtos.MiddleName,
                LastName = userDetailsAnkitDtos.LastName,
                Email = EncriptionAndDecription.EncryptData(userDetailsAnkitDtos.Email),
                Gender = userDetailsAnkitDtos.Gender,
                DateOfjoining = userDetailsAnkitDtos.DateOfjoining,
                Dob = userDetailsAnkitDtos.Dob,
                Phone = EncriptionAndDecription.EncryptData(userDetailsAnkitDtos.Phone),
                AlternatePhone = EncriptionAndDecription.EncryptData(userDetailsAnkitDtos.AlternatePhone),
                IsActive = userDetailsAnkitDtos.IsActive,
                ImagePath = "/book/Users",
                Password = EncriptionAndDecription.EncryptData(uniquePassword),
                UserAddressAnkits = userDetailsAnkitDtos.UserAddressAnkits.Select(a => new UserAddressAnkit
                {
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    ZipCode = a.ZipCode,
                    AddressId = a.AddressId,
                    CreatedDate = a.CreatedDate,
                    UpdateDate = a.UpdateDate,
                }).ToList()
            };

            return await _repository.AddingUserInDb(convertedfile);
        }


        public async Task<ResponseDto> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }

        public async Task<bool> LoginCredentialChecking(LoginCredentialDto loginCredentialDto)
        {
            var transformingData = new LoginCredentials()
            {
                Email = EncriptionAndDecription.EncryptData(loginCredentialDto.Email),
                Password = EncriptionAndDecription.EncryptData(loginCredentialDto.Password),
            };

            return await _repository.LoginCredentialChecking(transformingData);
        }

        public async Task<ResponseDto> DeleteUserDetail(int Id)
        {
            return await _repository.DeleteUserDetail(Id);
        }

        public async Task<ResponseDto> SendEmailToForgotPassword(string email)
        {
            try
            {   
                var encrypt = EncriptionAndDecription.EncryptData(email);
                var isPresent = await _repository.EmailIsPresentOrNot(encrypt);
                if (isPresent)
                {
                    //var tokenBytes = RandomNumberGenerator.GetBytes(64);
                    //var emailToken = Convert.ToBase64String(tokenBytes);
            
                    var subj = "Click link below to change password";
                    var body = "http://localhost:4200/auth/resetoldpassword";
                    await _emailSenderService.SendEmailAsync(email, subj, body);
                    return new ResponseDto { Data = null, Message = "Email sent successfully", StatusCode = 200 };
                }
                return new ResponseDto { Data = null, StatusCode = 401, Message = "You are not registered user" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message=ex.Message, StatusCode = 500 };
            }
        }

        public async Task<ResponseDto> UpdateUserDetail(UserDetailsAnkitDtos detailsAnkitDtos)
        {
            return await _repository.UpdateUserDetail(detailsAnkitDtos);
        }
    }
}
