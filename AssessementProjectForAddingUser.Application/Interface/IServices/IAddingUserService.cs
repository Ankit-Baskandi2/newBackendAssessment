using AssessementProjectForAddingUser.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Application.Interface.IServices
{
    public interface IAddingUserService
    {
        Task<string> AddingUserInDb(UserDetailsAnkitDtos userDetailsAnkitDtos);
    }
}
