using LittleLive.WebApi.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace LittleLive.WebApi.Middlewares
{
    public class DeviceExtractionMiddleware
    {
        private readonly RequestDelegate _next;

        public DeviceExtractionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context, IDetectionService detection)
        {
            context.Request.Headers[GeneralConstants.DeviceExtraction] = detection.Device.Type.ToString();
            await _next(context);
        }
    }
}
