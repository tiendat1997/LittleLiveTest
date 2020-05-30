using LittleLive.Core;
using LittleLive.Core.Repositories;
using LittleLive.Core.Services;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Service
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClassService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool IsClassBelongToSchool(Guid classId, Guid schoolId)
        {
            var efClass = _unitOfWork.Classes.SingleOrDefault(c => c.Id.Equals(classId) && c.SchoolId.Equals(schoolId));
            return efClass != null;
        }

        public bool IsClassTeachByUser(Guid classId, Guid teacherId)
        {
            var efClass = _unitOfWork.Classes.SingleOrDefault(c => c.Id.Equals(classId) && c.TeacherId.Equals(teacherId));
            return efClass != null;
        }
    }
}
