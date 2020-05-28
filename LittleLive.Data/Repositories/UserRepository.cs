using LittleLive.Core.Models;
using LittleLive.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
