using LittleLive.Core.Constants;
using LittleLive.Core.Models;
using LittleLive.Core.Services;
using LittleLive.WebApi.Constants;
using LittleLive.WebApi.Features;
using LittleLive.WebApi.Validators;
using LittleLive.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.FeatureManagement.Mvc;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [FeatureGate(LittleLiveFeatureFlags.DeviceTypeFlag)]
    public class ActivityController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly TeacherActivityExportRequestValidator _teacherActivityExportValidator;
        private readonly SchoolOwnerActivityExportRequestValidator _schoolOwnerActivityExportValidator;
        private readonly HQOwnerActivityExportRequestValidator _hqOwnerActivityExportValidator;
        public ActivityController(
            ITrackService trackService,
            TeacherActivityExportRequestValidator teacherActivityExportValidator,
            SchoolOwnerActivityExportRequestValidator schoolOwnerActivityExportValidator,
            HQOwnerActivityExportRequestValidator hqOwnerActivityExportValidator)
        {
            _trackService = trackService;
            _teacherActivityExportValidator = teacherActivityExportValidator;
            _schoolOwnerActivityExportValidator = schoolOwnerActivityExportValidator;
            _hqOwnerActivityExportValidator = hqOwnerActivityExportValidator;
        }

        // NORMAL PLAN
        [HttpPost]
        [Route("ExportForTeacher")]
        [Authorize(Policy = Policies.Teacher)]      
        public async Task<IActionResult> ExportActivityForTeacher(TeacherActivityExportRequest model)
        {
            try
            {
                model.UserId = new Guid(User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_USER_ID)).Value);
                var validationResult = await _teacherActivityExportValidator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                
                byte[] bytes = await _trackService.ExportActivityForTeacher(model.UserId, model.ClassId);
                return File(bytes, ActivityExportByTeacherConstant.FileFormat, ActivityExportByTeacherConstant.Title);
            }
            catch (System.Exception ex)
            {
                // IMPLEMENT LOGGING HERE ...
                return BadRequest(new BaseErrorMessage { Code = "export_failure", Message = ex.Message });
            }
        }

        // MEDIUM PLAN
        [HttpPost]
        [Route("ExportForSchoolOwner")]
        [Authorize(Policy = Policies.SchoolOwner)]
        public async Task<IActionResult> ExportActivityForSchoolOwner(SchoolOwnerActivityExportRequest model)
        {
            try
            {
                model.UserId = new Guid(User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_USER_ID)).Value);
                var validationResult = await _schoolOwnerActivityExportValidator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                byte[] bytes = await _trackService.ExportActivityForSchoolOnwer(model.UserId, model.SchoolId, model.ClassId);
                return File(bytes, ActivityExportBySchoolOwner.FileFormat, ActivityExportBySchoolOwner.Title);
            }
            catch (System.Exception ex)
            {
                // IMPLEMENT LOGGING HERE ...
                return BadRequest(new BaseErrorMessage { Code = "export_failure", Message = ex.Message });
            }
        }

        // ENTERPRISE PLAN
        [HttpPost]
        [Route("ExportForHQOwner")]
        [Authorize(Policy = Policies.HQOwner)]
        public async Task<IActionResult> ExportForHeadQuarterOwner(HQOwnerActivityExportRequest model)
        {
            try
            {
                model.UserId = new Guid(User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_USER_ID)).Value);
                var validationResult = await _hqOwnerActivityExportValidator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                byte[] bytes = await _trackService.ExportActivityForHQOwner(model.UserId, model.HeadQuarterId, model.SchoolId);
                return File(bytes, ActivityExportByHQOwner.FileFormat, ActivityExportByHQOwner.Title);
            }
            catch (System.Exception ex)
            {
                // IMPLEMENT LOGGING HERE... 
                return BadRequest(new BaseErrorMessage { Code = "export_failure", Message = ex.Message });
            }
        }
    }
}