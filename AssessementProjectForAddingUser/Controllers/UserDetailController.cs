using AssessementProjectForAddingUser.Application.Interface.IServices;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Infrastructure.CustomLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessementProjectForAddingUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IAddingUserService _addingUserService;
        private readonly IConfiguration _config;

        public UserDetailController(IAddingUserService addingUserService, IConfiguration config)
        {
            _addingUserService = addingUserService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] UserDetailsAnkitDtos userDetailsAnkitDtos)
        {       
            return Ok(await _addingUserService.AddingUserInDb(userDetailsAnkitDtos));
        }

        [HttpPost("UserLoginChecking")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromBody] LoginCredentialDto loginCredential)
        {
            var result = await _addingUserService.LoginCredentialChecking(loginCredential);
            if(result == true)
            {
                TokenGenerationService tokenGeneration = new TokenGenerationService(_config);
                var token = tokenGeneration.GenerateToken(loginCredential);
                return Ok(new ResponseDto { Data = token, Message = "Welcome back", StatusCode = 200 });
            }
            return BadRequest(new ResponseDto { Data = null, Message = "Incorrect email/password", StatusCode=401});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserDetails()
        {
            return Ok(await _addingUserService.GetAllUsers());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserDetails(int Id)
        {
            return Ok(await _addingUserService.DeleteUserDetail(Id));
        }

        [HttpPost("SendEmailToForgotPassword/{email}")]
        public async Task<IActionResult> SendingEmail(string email)
        {  
            return Ok(await _addingUserService.SendEmailToForgotPassword(email));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            
        }
    }
}
