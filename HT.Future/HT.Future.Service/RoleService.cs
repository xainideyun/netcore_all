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

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await TableNoTracking.Include(a => a.Routes).ToListAsync();
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var routes = await DbContext.Set<AccessAuthority>()
                .Where(a => a.RoleId == role.Id)
                .Select(a => new AccessAuthority { Id = a.Id })
                .ToListAsync();
            DbContext.RemoveRange(routes);
            DbContext.SaveChanges();
            role.ModifyTime = DateTime.Now;
            await UpdateAsync(role);
            return role;
        }

        public async Task<List<RoleUser>> AddRoleUsersAsync(IEnumerable<RoleUser> roleUsers)
        {
            var userId = roleUsers.First().UserId;
            var exist = await DbContext.Set<RoleUser>().Where(a => a.UserId == userId).ToListAsync();
            if (exist.Count > 0)
            {
                DbContext.RemoveRange(exist);
                await DbContext.SaveChangesAsync();
            }
            await DbContext.Set<RoleUser>().AddRangeAsync(roleUsers);
            await DbContext.SaveChangesAsync();
            return roleUsers.ToList();
        }

    }
}
