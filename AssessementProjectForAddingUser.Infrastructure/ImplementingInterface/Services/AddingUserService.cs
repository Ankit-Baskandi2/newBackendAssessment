using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Application.Interface.IServices;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
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

        public AddingUserService(IAddingUserDetailRepository addingUserDetail)
        {
            _repository = addingUserDetail; 
        }

        public async Task<string> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos)
        {
            var convertedfile = new UserDetailsAnkit
            {
                FirstName = userDetailsAnkitDtos.FirstName,
                MiddleName = userDetailsAnkitDtos.MiddleName,
                LastName = userDetailsAnkitDtos.LastName,
                Email = userDetailsAnkitDtos.Email,
                Gender = userDetailsAnkitDtos.Gender,
                DateOfjoining = userDetailsAnkitDtos.DateOfjoining,
                Dob = userDetailsAnkitDtos.Dob,
                Phone = userDetailsAnkitDtos.Phone,
                AlternatePhone = userDetailsAnkitDtos.AlternatePhone,
                IsActive = userDetailsAnkitDtos.IsActive,
                ImagePath = "/book/Users",
                Password = userDetailsAnkitDtos.Password,
                UserAddressAnkits = userDetailsAnkitDtos.UserAddressAnkits.Select(a => new UserAddressAnkit
                {
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    ZipCode = a.ZipCode,
                    AddressId = a.AddressId,
                }).ToList()
            };
            //var convertingToModal = userDetailsAnkitDtos.Adapt<UserDetailsAnkit>();
            return await _repository.AddingUserInDb(convertedfile);
        }
    }
}
