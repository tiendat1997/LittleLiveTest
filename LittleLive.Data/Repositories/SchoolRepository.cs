using LittleLive.Core.Entities;
using LittleLive.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
