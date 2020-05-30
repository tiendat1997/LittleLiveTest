using FluentValidation;
using LittleLive.Core.Entities;
using LittleLive.Core.Service;
using LittleLive.WebApi.ViewModels;
using System;
using LittleLive.Service.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Validators
{
    public class HQOwnerActivityExportRequestValidator : AbstractValidator<HQOwnerActivityExportRequest>
    {
        private readonly ISchoolService _schoolService;
        public HQOwnerActivityExportRequestValidator(ISchoolService schoolService)
        {
            _schoolService = schoolService;
            
            RuleFor(s => s.HeadQuarterId)
                    .NotEqual(Guid.Empty).WithMessage("Head Quarter Id is required");

            RuleFor(c => c)
                .Must(VerifySchoolOwnByUser).WithMessage("User is not the owner of the school");

            RuleFor(s => s)
               .Must(VerifyRequireSchoolIdForPaymentFree).WithMessage("School Id is required for the Free Plan");

            RuleFor(s => s)
                .Must(VerifySchoolBelongToHeadQuarter).WithMessage("School does not belong to Head Quarter");
        }

        private bool VerifyRequireSchoolIdForPaymentFree(HQOwnerActivityExportRequest model)
        {
            var school = _schoolService.GetById(model.HeadQuarterId);
            if (school == null)
            {
                return true;
            }
            else if (school.PaymentType.Equals(PaymentType.Free) && model.SchoolId.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        private bool VerifySchoolBelongToHeadQuarter(HQOwnerActivityExportRequest model)
        {
            if (model.SchoolId.HasValue && !model.SchoolId.IsNullOrEmpty())
            {
                return _schoolService.IsSchoolBelongToHeadQuarter(model.SchoolId.Value, model.HeadQuarterId);
            }

            return true;
        }

        private bool VerifySchoolOwnByUser(HQOwnerActivityExportRequest model)
        {
            return _schoolService.IsSchoolOwnByUserId(model.HeadQuarterId, model.UserId);
        }
    }
}
