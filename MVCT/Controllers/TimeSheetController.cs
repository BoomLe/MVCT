
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
    public class TimeSheetController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TimeSheetController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> CheckInAsync(DateTime dateCheckIn)
        {
            return View();
        }

        // hàm này để gửi yêu cầu check in bên button checkin
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> CheckInForUserAsync(DateTime dateCheckIn)
        {
           // lấy user ra để check in cho nó
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            // niếu check in nữa thì khum đc
            Timesheets check = _context.Timesheets.FirstOrDefault(c => c.UserId == currentUser.Id &&
                                                       dateCheckIn.Day == c.CreatedDate.Day &&
                                                       dateCheckIn.Month == c.CreatedDate.Month &&
                                                       dateCheckIn.Year == c.CreatedDate.Year);
            if (check != null)
            {
                return View("Checked");
            }

            //  tạo cho nó cái phiên làm việc của ngày đó
            Timesheets t = new Timesheets()
            {
                UserId = currentUser.Id,
                CheckIn = true,
                CreatedDate = dateCheckIn,
                State = "No"
            };
            _context.Timesheets.Add(t);
            _context.SaveChanges();
            return   View("Checked");
        }

        // hàm này show lịch sử check in-out
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> IndexAsync()
        {

            TimeSheetsDTO tmp = new TimeSheetsDTO();

            // lấy user hiện tại đang đăng  nhập
            var currentUser = await _userManager.GetUserAsync(User);

            List<Timesheets> Timekeepings = new List<Timesheets>();

            List<Timesheets> timekeepings = await _context.Timesheets
            .Where(t => t.UserId == currentUser.Id)
            .ToListAsync();

            ViewBag.Timekeepings = timekeepings;
            return View(tmp);
        }


        // cái hàm này để check out
        [HttpPost]
        public async Task<IActionResult> checkInTimeSheetAsync(TimeSheetsDTO checkIn)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                Timesheets check = _context.Timesheets.FirstOrDefault(c => c.UserId == currentUser.Id &&
                                                          checkIn.CreatedDate.Day == c.CreatedDate.Day &&
                                                          checkIn.CreatedDate.Month == c.CreatedDate.Month &&
                                                          checkIn.CreatedDate.Year == c.CreatedDate.Year);
                if(check == null)
                {
                    return View("CheckIn");
                }
                // niếu chưa dược duyệt thì có thể sửa
                if (check.State != "No")
                {
                    return View("DenyCheckOut");
                }

                // cập nhật lại cái phiên làm việc ngày hôm đó
              
                check.WorkingContent = checkIn.WorkingContent;
                check.TimeCheckout = checkIn.TimeCheckout;

                int totalMinutes = 0;
                string time = "";
                TimeSpan? difference = check.TimeCheckout - check.CreatedDate;
                if (difference.HasValue)
                {
                    totalMinutes = (int)difference.Value.TotalMinutes;
                    int hour = totalMinutes / 60;
                    int minute = totalMinutes % 60;
                    time =  hour + ":" + minute;
                }


                check.TimeWork = time;
                _context.Update(check);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thêm mới Timesheet: " + ex.Message;

                return Content("lỗi rồi" + ex);
            }
        }
        public string GetDistanceTwoDateTime(DateTime a, DateTime b)
        {
            int totalMinutes = 0;
            TimeSpan? difference = a - b;
            if (difference.HasValue)
            {
                totalMinutes = (int)difference.Value.TotalMinutes;
                int hour = totalMinutes / 60;
                int minute = totalMinutes % 60;
                return hour + ":" + minute;
            }

            return "";
        }


        // lấy đám nhân viên của ngày đó để chấm công
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ManageUserTimeSheets()
        {
            DateTime selectedDate = TempData.ContainsKey("SelectedDate") && TempData["SelectedDate"] is DateTime tempDate
                                     ? tempDate
                                     : DateTime.Today;
            // lấy danh sách chấm công trong ngày ( đã check in và out
            List<Timesheets> Timekeepings = _context.Timesheets.Where(t => selectedDate.Day == t.CreatedDate.Day &&
                                                          selectedDate.Month == t.CreatedDate.Month &&
                                                          selectedDate.Year == t.CreatedDate.Year
                                                          && t.TimeWork != null).ToList();
     
                List<AppUser> users = new List<AppUser>();
                foreach (Timesheets t in Timekeepings)
                {
                    AppUser user = await _userManager.FindByIdAsync(t.UserId);
                    if (user != null)
                    {
                        users.Add(user);
                    }
                }

               // kết hợp lại, tao danh sách user để chấm công
               List<TimeSheetsDTO> listUsersCheckIn = new List<TimeSheetsDTO>();
                foreach (Timesheets t in Timekeepings)
                {
                    foreach (AppUser user in users)
                    {
                        if (t.UserId == user.Id)
                        {
                            string userNameDTO = user.UserName;
                            TimeSheetsDTO tmp = new TimeSheetsDTO()
                            {
                                Id = t.Id,
                                UserName = user.Name,
                                CheckIn = (bool)t.CheckIn,
                                WorkingContent = t.WorkingContent,
                                CreatedDate = t.CreatedDate,
                                TimeWork = t.TimeWork,
                                State = t.State,
                                TimeCheckout = t.TimeCheckout
                            };
                            listUsersCheckIn.Add(tmp);


                         }
                    }

                }

                TimeSheetsDTO tmp2 = new TimeSheetsDTO()
                {
                    CheckIn = true,
                    TimeWork = "",
                    WorkingContent = "",
                    CreatedDate = DateTime.Now,
                };
                ViewBag.listUsersCheckIn = listUsersCheckIn;
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                ViewBag.managerCheckOut = currentUser.Id;
                ViewBag.selectedDate = selectedDate;
            
            return View(tmp2);              
        }


        // cái hàm này dùng dể selet cái ngày để render đám nhân viên
        [HttpPost]
        public IActionResult GetUsersTimeKeeping(DateTime selectedDate)
        {
            TempData["SelectedDate"] = selectedDate;
            return RedirectToAction("ManageUserTimeSheets");
        }



        // hàm này dùng để xử lý việc duyệt check out cho user
        [HttpPost("/save-check-out/")]
        public IActionResult AcceptCheckouts([FromBody] CheckoutData data)
        {
            List<int> highlightedCheckout = data.highlightedCheckout;
            DateTime date = data.date;
            string idManagerChectOut = data.idManagerCheckOut;
           
            List<Timesheets> ls = new List<Timesheets>();
            foreach (int i in highlightedCheckout)
            {
                Timesheets tmp = _context.Timesheets.Find(i);

                if (tmp != null)
                {
                    if (tmp.State == "Yes")
                    {
                        tmp.State = "No"; 
                        tmp.UserCheckId = null;
                    }
                    else
                    {
                        tmp.State = "Yes"; 
                        tmp.UserCheckId = idManagerChectOut;
                    }
                }
                _context.Timesheets.Update(tmp);
                _context.SaveChanges();
            }
           

            return Json(new { Success = true, Message = "Gửi dữ liệu thành công!" });          
        }

        public IActionResult alterChangeTimeCheckInOut(AlterTimeSheet data)
        {
            Timesheets tmp = _context.Timesheets.Find(data.Id);
            if (tmp != null) {
                tmp.CreatedDate = (DateTime)data.dateCheckIn;
                tmp.TimeCheckout = data.TimeCheckout;

                // set up lại time
                int totalMinutes = 0;
                string time = "";
                TimeSpan? difference = tmp.TimeCheckout - tmp.CreatedDate;
                if (difference.HasValue)
                {
                    totalMinutes = (int)difference.Value.TotalMinutes;
                    int hour = totalMinutes / 60;
                    int minute = totalMinutes % 60;
                    time = hour + ":" + minute;
                }


                tmp.TimeWork = time;


                _context.Timesheets.Update(tmp);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageUserTimeSheets");
        }


        public IActionResult DeleteTimeSheet(int Id)
        {
            
            try
            {
                Timesheets tmp = _context.Timesheets.Find(Id);
                if (tmp != null)
                {
                    _context.Timesheets.Remove(tmp);
                    _context.SaveChanges();
                    return RedirectToAction("ManageUserTimeSheets");
                }
                return Content("lỗi xóa");
            }
            catch (Exception ex)
            {
                return Content("Lỗi" + ex.ToString());
            }
        }
    }

   
}
