using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using AssessementProjectForAddingUser.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseDto> AddingUserInDb(UserDetailsAnkit userAddress)
        {
            try
            {
                _context.UserDetailsAnkits.Add(userAddress);
                await _context.SaveChangesAsync();
                return new ResponseDto { Data = null, Message = "Data save successfully", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = 401 };
            }

        }

        public async Task<ResponseDto> DeleteUserDetail(int Id)
        {
            try
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
                return new ResponseDto { Data = null, Message =  message, StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = 401 };
            }

        }

        public async Task<bool> EmailIsPresentOrNot(string email)
        {
            try
            {
                bool isPresent = await _context.UserDetailsAnkits.AnyAsync(x => x.Email == email);
                return isPresent;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<ResponseDto> GetAllUsers()
        {  
            try
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
                return new ResponseDto { Data = collection, Message = "", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = 401 };
            }


        }

        public async Task<bool> LoginCredentialChecking(LoginCredentials loginCredential)
        {
            try
            {
                bool exists = await _context.UserDetailsAnkits.AnyAsync(u => u.Email == loginCredential.Email && u.Password == loginCredential.Password && u.IsActive == true);
                return exists;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //public Task<ResponseDto> ChangePassword(string oldPassword, string newPassword)
        //{
        //    try
        //    {
                
        //    }catch (Exception ex)
        //    {

        //    }
        //}


        public async Task<ResponseDto> UpdateUserDetail(UserDetailsAnkitDtos userDetailsAnkitDto)
        {
            try
            {
                var user = await _context.UserDetailsAnkits.Include(u => u.UserAddressAnkits).FirstOrDefaultAsync(u => u.UserId == userDetailsAnkitDto.Id);

                if (user != null)
                {
                    // Update user details
                    user.FirstName = userDetailsAnkitDto.FirstName;
                    user.MiddleName = userDetailsAnkitDto.MiddleName;
                    user.LastName = userDetailsAnkitDto.LastName;
                    user.Phone = EncriptionAndDecription.EncryptData(userDetailsAnkitDto.Phone);
                    user.AlternatePhone = EncriptionAndDecription.EncryptData(userDetailsAnkitDto.AlternatePhone);
                    user.DateOfjoining = userDetailsAnkitDto?.DateOfjoining;
                    user.Dob = userDetailsAnkitDto.Dob;
                    user.Gender = userDetailsAnkitDto.Gender;
                    user.Email = EncriptionAndDecription.EncryptData(userDetailsAnkitDto.Email);
                    user.IsActive = userDetailsAnkitDto.IsActive;

                    // Update or add user addresses
                    foreach (var addressDto in userDetailsAnkitDto.UserAddressAnkits)
                    {
                        var address = user.UserAddressAnkits.FirstOrDefault(a => a.AddressId == addressDto.AddressId);
                        if (address != null)
                        {
                            // Update existing address
                            address.City = addressDto.City;
                            address.State = addressDto.State;
                            address.Country = addressDto.Country;
                            address.ZipCode = addressDto.ZipCode;
                        }
                        else
                        {
                            // Add new address
                            user.UserAddressAnkits.Add(new UserAddressAnkit
                            {
                                City = addressDto.City,
                                State = addressDto.State,
                                Country = addressDto.Country,
                                ZipCode = addressDto.ZipCode,
                                Userid = user.UserId
                            });
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    return new ResponseDto { Data = null, Message = "Data updated Successfully", StatusCode = 200 };
                }
                return new ResponseDto { Data = null, Message = "Data not found", StatusCode = 404 };
            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = 500 };
            }
        }
    }
}
