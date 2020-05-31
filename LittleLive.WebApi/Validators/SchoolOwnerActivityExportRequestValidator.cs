using FluentValidation;
using LittleLive.Core.Entities;
using LittleLive.Core.Service;
using LittleLive.Core.Services;
using LittleLive.Service.Extensions;
using LittleLive.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Validators
{
    public class SchoolOwnerActivityExportRequestValidator : AbstractValidator<SchoolOwnerActivityExportRequest>
    {
        private readonly IClassService _classService;
        private readonly ISchoolService _schoolService;
        public SchoolOwnerActivityExportRequestValidator(IClassService classService, ISchoolService schoolService)
        {
            _classService = classService;
            _schoolService = schoolService;

            RuleFor(a => a.SchoolId)
                    .NotEqual(Guid.Empty).WithMessage("SchoolId is required");

            RuleFor(c => c)
                .Must(VerifySchoolOwnByUser).WithMessage("User is not the owner of the school");            

            RuleFor(c => c)
                .Must(VerifyRequireClassIdForPaymentFree).WithMessage("The ClassId is required for the Free Plan");

            RuleFor(c => c)
                .Must(VerifyClassBelongToTheSchool).WithMessage("Class does not belong to the school");
        }

        private bool VerifySchoolOwnByUser(SchoolOwnerActivityExportRequest model)
        {
            return _schoolService.IsSchoolOwnByUserId(model.SchoolId, model.UserId);
        }

        private bool VerifyRequireClassIdForPaymentFree(SchoolOwnerActivityExportRequest model)
        {
            var school = _schoolService.GetById(model.SchoolId);
            if (school == null)
            {
                return true;
            } 
            else if (school.SchoolPayment.Equals(SchoolPayment.Free) && model.ClassId.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        private bool VerifyClassBelongToTheSchool(SchoolOwnerActivityExportRequest model)
        {
            if (model.ClassId.HasValue && !model.ClassId.IsNullOrEmpty())
            {
                return _classService.IsClassBelongToSchool(model.ClassId.Value, model.SchoolId);
            }

            return true;
        }
    }
}
