using LittleLive.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Service
{
    public interface ISchoolService
    {
        bool IsSchoolOwnByUserId(Guid schoolId, Guid ownerId);
        bool IsSchoolBelongToHeadQuarter(Guid schoolId, Guid headQuarterId);        
        Task<IEnumerable<School>> GetAllSchools();
        School GetById(Guid schoolId);
    }
}
