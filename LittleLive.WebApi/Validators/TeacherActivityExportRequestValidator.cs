using FluentValidation;
using LittleLive.Core.Services;
using LittleLive.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Validators
{
    public class TeacherActivityExportRequestValidator : AbstractValidator<TeacherActivityExportRequest>
    {
        private readonly IClassService _classService;        
        public TeacherActivityExportRequestValidator(IClassService classService)
        {
            _classService = classService;

            RuleFor(a => a.ClassId)
                .NotEqual(Guid.Empty).WithMessage("ClassId is required");
            RuleFor(c => c)
                .Must(VerifyClassTeachByUser).WithMessage("Teacher does not teach the class"); 
        }

        private bool VerifyClassTeachByUser(TeacherActivityExportRequest model)
        {
            return _classService.IsClassTeachByUser(model.ClassId, model.UserId);
        }
    }
}
