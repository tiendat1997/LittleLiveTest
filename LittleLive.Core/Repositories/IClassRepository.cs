using LittleLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLive.Core.Repositories
{
    public interface IClassRepository : IRepository<Class>
    {
        Task<IEnumerable<Class>> GetWithSchoolId(Guid schoolId);
        Task<IEnumerable<Class>> GetWithSchoolIds(List<Guid> schoolIds);
    }
}
