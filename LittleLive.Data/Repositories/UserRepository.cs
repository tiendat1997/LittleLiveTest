using LittleLive.Core.Entities;
using LittleLive.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LittleLiveDbContext context)
            : base(context)
        { }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }

        public async Task<User> GetUserInformation(string userName, string password)
        {
            User efUser = await this.DbSet.Where(u => u.UserName.Equals(userName) && u.Password.Equals(password))
                                            .Include(u => u.Country)
                                            .SingleOrDefaultAsync();
            return efUser;
        }

        public async Task<IEnumerable<User>> GetUsersByCountryCode(string countryCode)
        {
            IEnumerable<User> efUsers = await this.DbSet.Where(u => u.Country.Code.Equals(countryCode)).ToListAsync();
            return efUsers;
        }
    }
}
