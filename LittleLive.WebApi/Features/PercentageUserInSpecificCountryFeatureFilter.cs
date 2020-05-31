using LittleLive.WebApi.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = LittleLive.Core.Models;

namespace LittleLive.WebApi.Features
{
    [FilterAlias(LittleLiveFilterAlias.PercentageUserInSpecificCountryFilter)]
    public class PercentageUserInSpecificCountryFeatureFilter : IFeatureFilter
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;        
        public PercentageUserInSpecificCountryFeatureFilter(
            IHttpContextAccessor httpContextAccessor,            
            IMemoryCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userIdClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_USER_ID)).Value;
            Guid userId = new Guid(userIdClaim);
            IEnumerable<Models.User> users = new List<Models.User>();
            // check if country code was changed 
            bool hitCache = _cache.TryGetValue(MemoryCacheItems.PercentageUsers, out users);            
            return Task.FromResult(hitCache && users.Any(u => u.Id.Equals(userId)));
        }       
    }

    public class PercentageUserInSpecificCountrySettings
    {
        public double Percent { get; set; }
        public string CountryCode { get; set; }
    }
}
