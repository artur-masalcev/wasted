using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

namespace DataAPI.Logging
{
    public class AnalyticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AnalyticsMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ControllerActionDescriptor descriptor = GetDescriptor(context);
            string controller = descriptor?.ControllerName;
            string action = descriptor?.ActionName;
            try
            {
                await _next(context);
                _logger.Information("Good");
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Failure: {controller}");
            }
        }
        
        private static ControllerActionDescriptor GetDescriptor(HttpContext context)
        {
            return context?
                .GetEndpoint()?
                .Metadata?
                .GetMetadata<ControllerActionDescriptor>();
        }
        
        
    }
}