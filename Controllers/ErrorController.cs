using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BrewBlissApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult General()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewBag.ErrorMessage = exceptionFeature?.Error.Message;
            return View("Error");
        }

        [Route("Error/404")]
        public IActionResult NotFoundError() => View("NotFound");

        [Route("Error/400")]
        public IActionResult BadRequestError() => View("BadRequest");
    }
}