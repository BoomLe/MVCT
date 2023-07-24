using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCT.Models;
using MVCT.Models.UserReport;
using MVCT.Repository.IReportRepository;

namespace MVCT.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        public ReportsController(IReportRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {
            List<UserReports> listReport = await _repository.GetAllAsync(includeProperties:"User");

            return View(listReport);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(UserReportDto model) 
        {
      
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                UserReports userModel = new ()
                {  
                     SendnDate = DateTime.Now,
                    Description = model.Description,
                    Messages = model.Messages,                   
                    //User = user
                    UserId = user.Id

                };
                await _repository.CreatedAsync(userModel);
         
                TempData["StatusMessage"] = "Thông tin đã gửi thành công";
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Lỗi không thể gửi";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _repository == null)
            {
                return NotFound();
            }

            var detailReport = await _repository.GetAsync(p => p.Id == id);
            if (detailReport == null)
            {
                return NotFound();
            }

            return View(detailReport);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            UserReports listReport = await _repository.GetAsync(p => p.Id == id);

            if (listReport == null)
            {
                return NotFound();
            }

            return View(listReport);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            UserReports reModel = await _repository.GetAsync(p=> p.Id == id);


            if (reModel == null)
            {
                return NotFound();
            }
            await _repository.RemoveAsync(reModel);

            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
