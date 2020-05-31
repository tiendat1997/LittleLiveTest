using LittleLive.Core.Models;
using LittleLive.Core.Service;
using LittleLive.Service.Extensions;
using LittleLive.WebApi.Constants;
using LittleLive.WebApi.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policies.Administrator)]
    public class FeatureController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IUserService _userService;
        public FeatureController(IMemoryCache cache, IUserService userService)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpPost]
        [Route("UpdatePercentageUserInSpecificCountryFlag")]
        public async Task<IActionResult> UpdatePercentageUserInSpecificCountryFlag(PercentageUserInSpecificCountrySettings settings)
        {
            try
            {
                IEnumerable<User> users = new List<User>();
                // check if country code was changed 
                if (!_cache.TryGetValue(MemoryCacheItems.PercentageUsersCountry, out string cachedCountryCode))
                {
                    _cache.Set(MemoryCacheItems.PercentageUsersCountry, settings.CountryCode, TimeSpan.FromDays(365));
                    // Update Cached Users
                    users = await _userService.GetUsersByCountryCode(settings.CountryCode);
                    users = users.TakePercent(settings.Percent);
                    _cache.Set(MemoryCacheItems.PercentageUsers, users, TimeSpan.FromDays(365));
                }
                else
                {
                    if (!cachedCountryCode.Equals(settings.CountryCode))
                    {
                        // Update Cached Users
                        users = await _userService.GetUsersByCountryCode(settings.CountryCode);
                        users = users.TakePercent(settings.Percent);
                        _cache.Set(MemoryCacheItems.PercentageUsers, users, TimeSpan.FromDays(365));
                    }
                }
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                // IMPLEMENT LOGGING HERE... 
                return BadRequest(new BaseErrorMessage { Code = "export_failure", Message = ex.Message });
            }
        }
    }
}