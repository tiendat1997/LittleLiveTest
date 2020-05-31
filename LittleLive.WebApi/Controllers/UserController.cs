using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleLive.WebApi.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace LittleLive.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    [Authorize(Policy = Policies.Administrator)]
    [FeatureGate(LittleLiveFeatureFlags.GlobalAccessFlag)]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [FeatureGate(RequirementType.Any,
            LittleLiveFeatureFlags.TrialUserFlag, 
            LittleLiveFeatureFlags.LatePaymentUserFlag)]
        [Route("GetUserData")]        
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }

        [HttpGet]
        [FeatureGate(RequirementType.All, LittleLiveFeatureFlags.LatePaymentUserFlag)]
        [Route("GetAdminData")]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from Admin method");
        }
    }
}
