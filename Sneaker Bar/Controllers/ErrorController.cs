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

        [Route("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode) {
                case 404:
                    logger.LogError($"Error {statusCode} occured. Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString= {statusCodeResult.OriginalQueryString}");
                    break;
            }    
            return View($"Error-{statusCode}");
        }

        [Route("/Error/")]
        [AllowAnonymous]
        public IActionResult Error() {
            try
            {
                var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                logger.LogError($"Path {exceptionDetails.Path} threw an exception " +
                    $"{exceptionDetails.Error}");
            }
            catch (Exception ex) {
                logger.LogError($"Exception was thrown: " +
                      $"{ex.Message}");
                ViewBag.title = "Error occured";
                ViewBag.message = "We work to fix this problem";
            }
            return View("Error");
        }


    }
}
