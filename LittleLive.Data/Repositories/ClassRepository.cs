using LittleLive.Core.Entities;
using LittleLive.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.Data.Repositories
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(LittleLiveDbContext context)
            : base(context)
        { }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }

        public async Task<IEnumerable<Class>> GetWithSchoolId(Guid schoolId)
        {
            List<Class> efClasses = await this.DbSet.Where(c => c.SchoolId.Equals(schoolId)).ToListAsync();

            return efClasses;
        }
    }
}
