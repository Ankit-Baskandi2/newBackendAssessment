using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using AssessementProjectForAddingUser.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AssessementProjectForAddingUser.Infrastructure.ImplementingInterface.Repositorys
{
    public class AddingUserDetailRepository : IAddingUserDetailRepository
    {
        private readonly TestContext _context;

        public AddingUserDetailRepository(TestContext context)
        {
            _context = context;
        }

        public async Task<string> AddingUserInDb(UserDetailsAnkit userAddress)
        {
           _context.UserDetailsAnkits.Add(userAddress);
            await _context.SaveChangesAsync();
            return "Data Successfully";
        }

        public async Task<string> DeleteUserDetail(int Id)
        {
            var messageParameter = new SqlParameter
            {
                ParameterName = "@message",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Direction = System.Data.ParameterDirection.Output,
                Size = 30
            };

            var parameter = new SqlParameter("@Id", Id);

            await _context.Database.ExecuteSqlRawAsync("EXEC UP_InActivateUser @Id, @message OUTPUT", parameter, messageParameter);

            var message = messageParameter.Value.ToString();
            return message;
        }

        public async Task<bool> EmailIsPresentOrNot(string email)
        {
            bool isPresent = await _context.UserDetailsAnkits.AnyAsync(x => x.Email == email);
            return isPresent;
        }

        public async Task<IEnumerable<UserDetailsAnkit>> GetAllUsers()
        {   
            var collection = _context.UserDetailsAnkits.Include(o => o.UserAddressAnkits).Select(o => new UserDetailsAnkit
            {
                UserId = o.UserId,
                FirstName = o.FirstName,
                MiddleName = o.MiddleName,
                LastName = o.LastName,
                Email = EncriptionAndDecription.DecryptData(o.Email),
                Gender = o.Gender,
                Phone = EncriptionAndDecription.DecryptData(o.Phone),
                AlternatePhone = EncriptionAndDecription.DecryptData(o.AlternatePhone),
                DateOfjoining = o.DateOfjoining,
                Dob = o.Dob,
                IsActive = o.IsActive,
                Password = EncriptionAndDecription.DecryptData(o.Password),
                UserAddressAnkits = o.UserAddressAnkits.Select(a => new UserAddressAnkit
                {
                    AddressId = a.AddressId,
                    Country = a.Country,
                    State = a.State,
                    City = a.City,
                    ZipCode = a.ZipCode,
                }).ToList()
            });
            return collection;

        }

        public async Task<bool> LoginCredentialChecking(LoginCredentials loginCredential)
        {
            bool exists = await _context.UserDetailsAnkits.AnyAsync(u => u.Email == loginCredential.Email && u.Password == loginCredential.Password && u.IsActive == true);
            return exists;
        }

        public Task<ResponseDto> ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                
            }catch (Exception ex)
            {

            }
        }


        //public Task<string> UpdateUserDetail(UserDetailsAnkit userDetailsAnkit)
        //{

        //}
    }
}
