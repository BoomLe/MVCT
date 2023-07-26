
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using MVCT.Data;
using MVCT.ExtendMethods;
using MVCT.Models;
using MVCT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCT.Models.User;
using MVCT.DTO;
using Newtonsoft.Json;
using System.Security.Cryptography;
namespace MVCT.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin,Manager")]
    public class StatisticalController : Controller 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public StatisticalController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexStatisticalTimeSheetOfWeek()
        {
            return View();
        }

        public IActionResult GetDataStatisticalCheckInOutOfMonth(int month, int year)
        {
            // Do something with month and year
            // Ví dụ: Truy vấn dữ liệu dựa trên tháng và năm đã chọn.
            List<Timesheets> tsOfMonth = _context.Timesheets.Where(t => t.CreatedDate.Month == month
            && t.CreatedDate.Year == year).ToList();

            //lấy số lượng pass
            int amountPass = 0;
            //lấy số lượng không pass
            int amountFail = 0;
            foreach (var item in tsOfMonth)
            {
                if(item.State == "Yes")
                {
                    amountPass++;
                }
                if(item.State == "No")
                {
                    amountFail++;   
                }    
            }

            //return Content($"Tháng: {month}, Năm: {year}");
            //var result = new
            //{
            //    passCheckOut = amountPass,
            //    noPassCheckOut = amountFail
            //};

            //return Json(result);
            ViewBag.PassCheckOut = amountPass;
            ViewBag.NoPassCheckOut = amountFail;

            ViewBag.MonthCurrentChoose = month;
            ViewBag.YearCurrentChoose = year;
            return View("IndexStatisticalTimeSheetOfWeek");
        }

    }
}
