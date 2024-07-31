using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using AssessementProjectForAddingUser.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;

namespace AssessementProjectForAddingUser.Infrastructure.ImplementingInterface.Repositorys
{
    public class AddingUserDetailRepository : IAddingUserDetailRepository
    {
        private readonly TestContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TokenGenerationService _tokenGenerationService;

        public AddingUserDetailRepository(TestContext context, IWebHostEnvironment webHostEnvironment, TokenGenerationService tokenGeneration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _tokenGenerationService = tokenGeneration;
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

        public async Task<ResponseDto> DeleteUserDetail(long id)
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

                var parameter = new SqlParameter("@Id", id);

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
                    ImagePath = o.ImagePath,
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

        public async Task<UserDetailsAnkit> GetUserByEmail(string email)
        {
            return await _context.UserDetailsAnkits.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<UserDetailsAnkit> GetUserById(long id)
        {
            return await _context.UserDetailsAnkits.FirstOrDefaultAsync(a => a.UserId == id);
        }

        public async Task<ResponseDto> LoginCredentialChecking(LoginCredentials loginCredential)
        {
            try
            {
                bool exists = await _context.UserDetailsAnkits.AnyAsync(u => u.Email == loginCredential.Email && u.Password == loginCredential.Password && u.IsActive == true);
                if(exists)
                {
                    var userDetails = _context.UserDetailsAnkits.FirstOrDefault(x => x.Email == loginCredential.Email);
                    
                    var token = _tokenGenerationService.GenerateToken(userDetails);
                    return new ResponseDto { Data =  token, Message = userDetails.FirstName+userDetails.LastName, StatusCode=200 };
                }
                return new ResponseDto { Data = null, Message = "You are not registered user", StatusCode = 401 };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = 500 };
            }
        }

        public async Task<ResponseDto> UpdatePassword(long id, ResetPasswordDto password)
        {
            try
            {
                var messageParameter = new SqlParameter
                {
                    ParameterName = "@message",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 40
                };

                var encriptPassword = EncriptionAndDecription.EncryptData(password.Password);
                var parameter1 = new SqlParameter("@Id", System.Data.SqlDbType.BigInt) { Value = id };
                var parameter2 = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 200) { Value = encriptPassword };

                await _context.Database.ExecuteSqlRawAsync("EXEC UP_UpdatePassword @Id, @password, @message OUTPUT",
                    parameter1, parameter2, messageParameter);
                var message = messageParameter.Value.ToString();
                return new ResponseDto { Data = null, Message = message, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data= null, Message = ex.Message,StatusCode = 401 };
            }
        }

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

        public async Task<ResponseDto> ChangePasswordWhenUserLogedIn(long id,ChangePasswordWhenLogedInDto ChangelogedInDto)
        {
            try
            {
                var encryptPass = EncriptionAndDecription.EncryptData(ChangelogedInDto.OldPassword);
                var userInDb = await _context.UserDetailsAnkits.Where(u => u.UserId == id).Select(u => new { u.Password }).FirstOrDefaultAsync();
                if (userInDb == null)
                    return new ResponseDto { Data = null, Message = "Record not found", StatusCode = 404 };
                if (userInDb.Password != encryptPass)
                    return new ResponseDto { Data = null, Message = "Old password is incorrect", StatusCode = 401 };

                var messageParameter = new SqlParameter
                {
                    ParameterName = "@message",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 40
                };

                var encriptPassword = EncriptionAndDecription.EncryptData(ChangelogedInDto.Password);
                var parameter1 = new SqlParameter("@Id", System.Data.SqlDbType.BigInt) { Value = id };
                var parameter2 = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 200) { Value = encriptPassword };

                await _context.Database.ExecuteSqlRawAsync("EXEC UP_UpdatePassword @Id, @password, @message OUTPUT",
                    parameter1, parameter2, messageParameter);
                var message = messageParameter.Value.ToString();
                return new ResponseDto { Data = null, Message = message, StatusCode = 200 };

            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message= ex.Message, StatusCode = 500 };
            }
        }
    }
}
