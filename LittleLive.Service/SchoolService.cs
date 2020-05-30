using AutoMapper;
using LittleLive.Core;
using LittleLive.Core.Models;
using LittleLive.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Entities = LittleLive.Core.Entities;

namespace LittleLive.Service
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SchoolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Task<IEnumerable<School>> GetAllSchools()
        {
            throw new NotImplementedException();
        }

        public School GetById(Guid schoolId)
        {
            Entities.School efSchool = _unitOfWork.Schools.SingleOrDefault(c => c.Id.Equals(schoolId));
            School school = _mapper.Map<Entities.School, School>(efSchool);
            return school;
        }

        public bool IsSchoolBelongToHeadQuarter(Guid schoolId, Guid headQuarterId)
        {
            var efSchool = _unitOfWork.Schools.SingleOrDefault(c => c.Id.Equals(schoolId) && c.ParentId.Equals(headQuarterId));
            return efSchool != null;
        }

        public bool IsSchoolOwnByUserId(Guid schoolId, Guid ownerId)
        {
            var efSchool = _unitOfWork.Schools.SingleOrDefault(c => c.Id.Equals(schoolId) && c.OwnerId.Equals(ownerId));
            return efSchool != null;
        }
    }
}
