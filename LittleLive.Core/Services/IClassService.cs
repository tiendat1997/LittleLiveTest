using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Services
{
    public interface IClassService
    {
        bool IsClassTeachByUser(Guid classId, Guid userId);
        bool IsClassBelongToSchool(Guid classId, Guid schoolId);
    }
}
