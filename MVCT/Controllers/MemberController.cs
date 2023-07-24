using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCT.Data;
using MVCT.Models;
using MVCT.Models.Attandance;
using MVCT.Models.Member;
using MVCT.Repository.ICheckinRepository;

namespace MVCT.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IAttandancesRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        [TempData]
        public string StatusMessage { get; set; }
        public MemberController(IAttandancesRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Attandances> attendancesTody = await _repository.GetAllAsync((p => p.UserId == user.Id && p.DataTime.Date == DateTime.Now.Date));

            DateTime? checkIn = null;
            DateTime? checkOut = null;

            if (attendancesTody.Any())
            {
                checkIn = attendancesTody.First().DataTime;

                if (attendancesTody.Count > 1)
                {
                    checkOut = attendancesTody.Last().DataTime;
                }
            }
            var model = new MemberAttandanceInfo
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //Tạo Post
            Attandances newpost = new Attandances()
            {
                DataTime = DateTime.Now,
                UserId = user.Id
            };
            await _repository.CreatedAsync(newpost);
            StatusMessage = "Check-In Thành công";
            return RedirectToAction(nameof(Index));
        }

    }
}
