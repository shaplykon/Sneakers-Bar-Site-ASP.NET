using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sneaker_Bar.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;
        public ErrorController(ILogger<ErrorController> _logger) {
            logger = _logger;
        }

        [HttpGet("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {         
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            this.HttpContext.Response.StatusCode = statusCode;
            switch (statusCode) {
                case 404:
                case 500:
                    logger.LogError($"Error {statusCode} occured. Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString= {statusCodeResult.OriginalQueryString}");
                    break;
                default:
                    return View("Error");
            }    
            return View($"Error-{statusCode}");
        }      
    }
}
