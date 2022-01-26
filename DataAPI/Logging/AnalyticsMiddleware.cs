using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

namespace DataAPI.Logging
{
    public class AnalyticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private string type { get; set; }
        private string username { get; set; }

        private bool first { get; set; }

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
            
            if (context.Request.Path.ToString().StartsWith("/placeusers"))
            {
                type = "Place user";
                username = String.Concat(context.Request.Path.ToString().Skip(12).TakeWhile(character => character != '/'));
            } 
            else if (context.Request.Path.ToString().StartsWith("/clientusers"))
            {
                type = "Client user";
                username = String.Concat(context.Request.Path.ToString().Skip(13).TakeWhile(character => character != '/'));
            }
            
            string log = formatLogMessage(
                context.Connection.RemoteIpAddress.ToString(),
                context.Request.Path,
                controller, action);
            
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (!e.Message.Equals("No route matches the supplied values."))
                {
                    _logger.Error(e, $"Failure: {controller}");
                }
                // await HandleException(context); 
                Console.WriteLine(e.StackTrace);
            }

            if (first == false)
            {
                first = true;
                return;
            }

            _logger.Information(log);
        }
        
        private Task HandleException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string errorType = "Something went wrong. Please contact developers.";

            return context.Response.WriteAsync(new {Message = errorType}.ToString());
        }
        
        private static ControllerActionDescriptor GetDescriptor(HttpContext context)
        {
            return context?
                .GetEndpoint()?
                .Metadata?
                .GetMetadata<ControllerActionDescriptor>();
        }
        
        public string formatLogMessage(string ip, string path, string controller, string action)
        {
            string controllerString = (controller == null) ? "" : $"\tController: {controller}\n";
            string actionString = (action == null) ? "" : $"\tAction: {action}\n";

            return $"\n\tUser type: {type}\n" +
                   $"\tUsername: {username}\n" +
                   $"\tIP address: {ip}\n" +
                   $"\tPath: {path}\n" +
                   controllerString +
                   actionString;
        }
        
    }
}