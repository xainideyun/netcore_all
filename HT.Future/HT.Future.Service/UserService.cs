using HT.Future.Entities;
using HT.Future.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HT.Future.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private ILogger<UserService> _logger;
        public UserService(HtDbContext dbContext, ILogger<UserService> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await TableNoTracking.FirstOrDefaultAsync(a => a.UserName == username.ToLower());
        }

        public async Task<List<Role>> GetRolesAsync(int userId)
        {
            var list = await DbContext.Set<RoleUser>()
                .AsNoTracking()
                .Include(a => a.Role)
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return list.Select(a => a.Role).ToList();
        }

    }
}
