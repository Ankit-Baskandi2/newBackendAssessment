using AssessementProjectForAddingUser.Application.Interface.IServices;
using AssessementProjectForAddingUser.Domain.DTOs;
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
    }
}
