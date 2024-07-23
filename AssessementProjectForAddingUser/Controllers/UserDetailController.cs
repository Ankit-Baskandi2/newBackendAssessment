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

        public UserDetailController(IAddingUserService addingUserService)
        {
            _addingUserService = addingUserService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] UserDetailsAnkitDtos userDetailsAnkitDtos)
        {       
            return Ok(await _addingUserService.AddingUserInDb(userDetailsAnkitDtos));
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("UserValidation")]
        //public async Task<IActionResult> UserValidation([FromBody] EmailAndPasswordModal emailAndPassword)
        //{
        //    if (!ModelState.IsValid)
        //        BadRequest(new ResponseModal { StatusCode = StaticData.errorStatusCode, Message = StaticData.errorMessage, Data = StaticData.data });

        //    var result = await _userSignUpService.ValidatingUserEmailAndPassword(emailAndPassword);

        //    if (result == 1)
        //    {
        //        TokenGenerationService tokenGeneration = new TokenGenerationService(_config);
        //        var token = tokenGeneration.GenerateToken(emailAndPassword);
        //        return Ok(new ResponseModal { StatusCode = StaticData.statusCode, Message = StaticData.successMessage, Data = token });
        //    }
        //    return BadRequest(new ResponseModal { StatusCode = StaticData.errorStatusCode, Message = StaticData.errorMessage, Data = StaticData.data });
        //}
    }
}
