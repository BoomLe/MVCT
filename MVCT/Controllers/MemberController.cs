using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCT.Data;

namespace MVCT.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
