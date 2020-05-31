using LittleLive.WebApi.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace LittleLive.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        [HttpGet]        
        [Route("GetSchoolProfile")]
        [FeatureGate(RequirementType.Any,
            LittleLiveFeatureFlags.NormalPlanFlag,
            LittleLiveFeatureFlags.PremiumPlanFlag,
            LittleLiveFeatureFlags.EnterprisePlanFlag
            )]
        public IActionResult GetSchoolProfile()
        {
            return Ok("This is your school profile");
        }

        [HttpGet]
        [Route("GetAllSchools")]
        [FeatureGate(RequirementType.Any,
           LittleLiveFeatureFlags.PremiumPlanFlag,
           LittleLiveFeatureFlags.EnterprisePlanFlag)]        
        public IActionResult GetAllSchools() // Include Head Quarters
        {
            return Ok("There are all of schools in your application ");
        }

        [HttpGet]      
        [Route("GetAllHeadQuarters")]
        [FeatureGate(RequirementType.Any,
         LittleLiveFeatureFlags.EnterprisePlanFlag)]
        public IActionResult GetAllHeadQuarters()
        {
            return Ok("There are all of head quarters in your application");
        }

       
    }
}