using HT.Future.Entities;
using HT.Future.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HT.Future.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(HtDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await TableNoTracking.FirstOrDefaultAsync(a => a.UserName == username.ToLower());
        }

    }
}
