using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Application.Interface.IServices;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos)
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
                }).ToList()
            };
 
            return await _repository.AddingUserInDb(convertedfile);
        }
    }
}
