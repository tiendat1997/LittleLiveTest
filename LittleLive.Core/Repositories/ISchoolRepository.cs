using LittleLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Repositories
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<IEnumerable<School>> FindIncludeClasses(Expression<Func<School, bool>> predicate);
        Task<School> GetByIdIncludeClasses(Guid id);
    }
}
