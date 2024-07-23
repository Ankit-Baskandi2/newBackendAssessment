using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Application.Interface.IRepositorys
{
    public interface IAddingUserDetailRepository
    {
        Task<string> AddingUserInDb(UserDetailsAnkit userAddress);
    }
}
