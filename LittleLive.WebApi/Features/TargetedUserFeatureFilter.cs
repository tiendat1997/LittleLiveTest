using LittleLive.Core.Entities;
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
    [FilterAlias(LittleLiveFilterAlias.TrialUserFilter)]
    public class TrialUserFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TrialUserFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string subscriptionTypeClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_SUBSCRIPTION_TYPE)).Value;
            var settings = context.Parameters.Get<SubscriptionFilterSettings>();

            return Task.FromResult(settings.SubscriptionType.Equals(subscriptionTypeClaim));
        }
    }

    [FilterAlias(LittleLiveFilterAlias.LatePaymentUserFilter)]
    public class LatePaymentUserFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LatePaymentUserFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string subscriptionTypeClaim = httpContext.User.Claims.First(s => s.Type.Equals(ClaimConfig.CLAIM_SUBSCRIPTION_TYPE)).Value;
            var settings = context.Parameters.Get<SubscriptionFilterSettings>();

            return Task.FromResult(settings.SubscriptionType.Equals(subscriptionTypeClaim));
        }
    }

    public class SubscriptionFilterSettings
    {
        public string SubscriptionType { get; set; }
    }
}
