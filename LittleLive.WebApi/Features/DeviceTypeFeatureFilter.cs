using LittleLive.WebApi.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace LittleLive.WebApi.Features
{
    [FilterAlias(LittleLiveFilterAlias.DeviceTypeFilter)]
    public class DeviceTypeFeatureFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeviceTypeFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(GeneralConstants.DeviceExtraction, out StringValues deviceType)) {
                
                DeviceTypeFilterSettings settings = context.Parameters.Get<DeviceTypeFilterSettings>();
                return Task.FromResult(settings.AllowedDevices.Contains(deviceType.ToString()));
            }

            return Task.FromResult(false);
        }
    }

    public class DeviceTypeFilterSettings
    {
        public string[] AllowedDevices { get; set; }
    }
}
