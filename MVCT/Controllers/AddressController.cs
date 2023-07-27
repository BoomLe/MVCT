using Microsoft.AspNetCore.Mvc;
using MVCT.Data;
using MVCT.Models;

namespace MVCT.Controllers
{
    public class AddressController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public AddressController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAddress()
        {
            List<Address> ls = _context.Addresses.ToList();
            return Json(ls);
        }
    }
}
