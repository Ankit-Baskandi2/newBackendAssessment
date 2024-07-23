using AssessementProjectForAddingUser.Application.Interface.IRepositorys;
using AssessementProjectForAddingUser.Domain.DTOs;
using AssessementProjectForAddingUser.Domain.Entity;
using AssessementProjectForAddingUser.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
