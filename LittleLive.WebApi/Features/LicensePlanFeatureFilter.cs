using LittleLive.WebApi.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Features
{
    [FilterAlias(LittleLiveFilterAlias.NormalPlanFilter)]
    public class NormalPlanFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NormalPlanFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string licensePlanClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_LICENSE_PLAN)).Value;
            var settings = context.Parameters.Get<LicensePlanFilterSettings>();

            return Task.FromResult(settings.LicensePlan.Equals(licensePlanClaim));
        }
    }

    [FilterAlias(LittleLiveFilterAlias.PremiumPlanFilter)]
    public class MediumPlanFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MediumPlanFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string licensePlanClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_LICENSE_PLAN)).Value;
            var settings = context.Parameters.Get<LicensePlanFilterSettings>();

            return Task.FromResult(settings.LicensePlan.Equals(licensePlanClaim));
        }
    }

    [FilterAlias(LittleLiveFilterAlias.EnterprisePlanFilter)]
    public class EnterprisePlanFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EnterprisePlanFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string licensePlanClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_LICENSE_PLAN)).Value;
            var settings = context.Parameters.Get<LicensePlanFilterSettings>();

            return Task.FromResult(settings.LicensePlan.Equals(licensePlanClaim));
        }
    }

    public class LicensePlanFilterSettings
    {
        public string LicensePlan { get; set; }
    }
}
