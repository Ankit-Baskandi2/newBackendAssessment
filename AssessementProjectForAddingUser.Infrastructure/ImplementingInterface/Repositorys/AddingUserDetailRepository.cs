using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using AssessementProjectForAddingUser.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using AssessementProjectForAddingUser.Domain.HelperClass;

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
                return new ResponseDto { Data = null, Message = ResponseMessageClass.addedSuccess, StatusCode = ResponseMessageClass.successStatusCode };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = ResponseMessageClass.unsuccessStatusCode };
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
                return new ResponseDto { Data = null, Message =  message, StatusCode = ResponseMessageClass.successStatusCode };
            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = ResponseMessageClass.unsuccessStatusCode };
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
                Console.WriteLine(ex.Message);
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
                return new ResponseDto { Data = collection, Message = ResponseMessageClass.emptyMessage, StatusCode = ResponseMessageClass.successStatusCode };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = ResponseMessageClass.unsuccessStatusCode };
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
                bool exists = await _context.UserDetailsAnkits.AnyAsync(u => u.Email == loginCredential.Email && u.Password == loginCredential.Password);


                if(exists)
                {
                    var userDetails = _context.UserDetailsAnkits.FirstOrDefault(x => x.Email == loginCredential.Email);
                    
                    var token = _tokenGenerationService.GenerateToken(userDetails);
                    return new ResponseDto { Data =  token, Message = userDetails.FirstName, StatusCode=ResponseMessageClass.successStatusCode };
                }
                return new ResponseDto { Data = null, Message = ResponseMessageClass.invalidUser, StatusCode = ResponseMessageClass.unsuccessStatusCode };
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
                return new ResponseDto { Data = null, Message = message, StatusCode = ResponseMessageClass.successStatusCode };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Data= null, Message = ex.Message,StatusCode = ResponseMessageClass.unsuccessStatusCode };
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
                    return new ResponseDto { Data = null, Message = ResponseMessageClass.updateSuccess, StatusCode = 200 };
                }
                return new ResponseDto { Data = null, Message = ResponseMessageClass.notFound, StatusCode = ResponseMessageClass.notFoundStatusCode };
            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message, StatusCode = ResponseMessageClass.badRequestStatusCode };
            }
        }

        public async Task<ResponseDto> ChangePasswordWhenUserLogedIn(long id,ChangePasswordWhenLogedInDto ChangelogedInDto)
        {
            try
            {
                var encryptPass = EncriptionAndDecription.EncryptData(ChangelogedInDto.OldPassword);
                var userInDb = await _context.UserDetailsAnkits.Where(u => u.UserId == id).Select(u => new { u.Password }).FirstOrDefaultAsync();
                if (userInDb == null)
                    return new ResponseDto { Data = null, Message = ResponseMessageClass.notFound, StatusCode = 404 };
                if (userInDb.Password != encryptPass)
                    return new ResponseDto { Data = null, Message = ResponseMessageClass.oldPasswordIncorrect, StatusCode = 401 };

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
                return new ResponseDto { Data = null, Message = message, StatusCode = ResponseMessageClass.successStatusCode };

            }
            catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message= ex.Message, StatusCode = ResponseMessageClass.badRequestStatusCode };
            }
        }

        public async Task<ResponseDto> GetDataThroughPagination(PaginationDto pagination)
        {

            try
            {
                var query = _context.UserDetailsAnkits.Include(u => u.UserAddressAnkits).AsQueryable();

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((decimal)totalCount/pagination.PageSize);

                if (!string.IsNullOrEmpty(pagination.Name))
                {
                    query = query.Where(x => x.FirstName.Contains(pagination.Name));
                }

                if (!string.IsNullOrEmpty(pagination.ContactNo))
                {
                    query = query.Where(x => x.Phone.Contains(EncriptionAndDecription.EncryptData(pagination.ContactNo)));
                }


                var dataList = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).
                    Select(user => new UserDetailsAnkit
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        Phone = EncriptionAndDecription.DecryptData(user.Phone),
                        AlternatePhone = EncriptionAndDecription.DecryptData(user.AlternatePhone),
                        Gender = user.Gender,
                        Email = EncriptionAndDecription.DecryptData(user.Email),
                        Dob = user.Dob,
                        DateOfjoining = user.DateOfjoining,
                        IsActive = user.IsActive,
                        ImagePath = user.ImagePath,
                        UserAddressAnkits = user.UserAddressAnkits.Select(a => new UserAddressAnkit
                        {
                            AddressId = a.AddressId,
                            Country = a.Country,
                            State = a.State,
                            City = a.City,
                            ZipCode = a.ZipCode
                        }).ToList()
                    }).ToListAsync();

                return new ResponseDto { Data = dataList, Message = ResponseMessageClass.emptyMessage,
                    StatusCode = ResponseMessageClass.successStatusCode };
            } catch(Exception ex)
            {
                return new ResponseDto { Data = null, Message = ex.Message,
                    StatusCode = ResponseMessageClass.unsuccessStatusCode };
            }
        }
    }
}
