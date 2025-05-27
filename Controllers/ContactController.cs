using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrewBlissApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        // GET: Contact
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        public IActionResult Contact(string type, string fullName, string email, string message)
        {
            _logger.LogInformation($"Message received from {fullName} ({email}) - Type: {type} - Message: {message}");

            TempData["SuccessMessage"] = "Thank you! Your message has been received.";
            return RedirectToAction("Contact");
        }
    }
}
