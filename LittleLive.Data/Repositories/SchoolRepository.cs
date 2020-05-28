using LittleLive.Core.Models;
using LittleLive.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Data.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(LittleLiveDbContext context)
            : base(context)
        { }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }
    }
}
