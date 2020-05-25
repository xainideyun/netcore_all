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
    public class RoleService : BaseService<Role>, IRoleService
    {
        private ILogger<RoleService> _logger;
        public RoleService(HtDbContext dbContext, ILogger<RoleService> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<object> GetUserMenuAuthAsync(int userId)
        {
            return null;
        }

        public async Task<List<RoleUser>> AddRoleUsersAsync(IEnumerable<RoleUser> roleUsers)
        {
            await DbContext.Set<RoleUser>().AddRangeAsync(roleUsers);
            await DbContext.SaveChangesAsync();
            return roleUsers.ToList();
        }

    }
}
