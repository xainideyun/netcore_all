using HT.Future.Entities;
using HT.Future.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

    }
}
