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
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(LittleLiveDbContext context)
            : base(context)
        { }

        private LittleLiveDbContext LittleLiveDbContext
        {
            get { return Context as LittleLiveDbContext; }
        }

        public async Task<IEnumerable<School>> FindIncludeClasses(Expression<Func<School, bool>> predicate)
        {
            IEnumerable<School> efResult = await Context.Set<School>().Where(predicate).Include(s => s.Classes).ToListAsync();
            return efResult;
        }

        public async Task<School> GetByIdIncludeClasses(Guid id)
        {
            School efResult = await this.DbSet.Where(s => s.Id.Equals(id))
                                              .Include("Classes")                                              
                                              .FirstOrDefaultAsync();
            return efResult;
        }
    }
}
