
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
    public class TimeSheetController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TimeSheetController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> CheckInAsync(DateTime dateCheckIn)
        {
            return View();
        }

        // hàm này để gửi yêu cầu check in bên button checkin
        [HttpPost]
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
            return RedirectToAction("Index");
        }


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

            //return View();

            return View(tmp);
        }


        // cái hàm này để check out
        [HttpPost]
        public async Task<IActionResult> checkInTimeSheetAsync(TimeSheetsDTO checkIn)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            //int day = checkIn.CreatedDate.Day;      // Lấy ngày trong tháng (1-31)
            //int month = checkIn.CreatedDate.Month;  // Lấy tháng trong năm (1-12)
            //int year = checkIn.CreatedDate.Year;
            try
            {
                Timesheets check = _context.Timesheets.FirstOrDefault(c => c.UserId == currentUser.Id &&
                                                          checkIn.CreatedDate.Day == c.CreatedDate.Day &&
                                                          checkIn.CreatedDate.Month == c.CreatedDate.Month &&
                                                          checkIn.CreatedDate.Year == c.CreatedDate.Year);
                if(check == null)
                {
                    return Content("Bạn chưa Check In");
                }
                // niếu chưa dược duyệt thì có thể sửa
                if (check.State != "No")
                {
                    return View("DenyCheckOut");
                }

                // cập nhật lại cái phiên làm viecj ngày hôm đó
                check.TimeWork = checkIn.TimeWork;
                check.WorkingContent = checkIn.WorkingContent;


                _context.Update(check);
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi thêm mới Timesheet: " + ex.Message;

                return Content("lỗi rồi" + ex);
            }
            //string json = JsonConvert.SerializeObject(t);

            //// Trả về chuỗi JSON trong phản hồi HTTP
            //return Content(json, "application/json");
        }



       // lấy đám nhân viên của ngày đó để chấm công
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

                //  lấy thêm tên của người ta nữa
                //1 lấy ds user ứng với id đó thôi
                //= _userManager.Users.ToList();
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
                                UserName = userNameDTO,
                                CheckIn = (bool)t.CheckIn,
                                WorkingContent = t.WorkingContent,
                                CreatedDate = t.CreatedDate,
                                TimeWork = (int)t.TimeWork,
                                State = t.State
                                
                            };
                            listUsersCheckIn.Add(tmp);


                         }
                    }

                }

                TimeSheetsDTO tmp2 = new TimeSheetsDTO()
                {
                    CheckIn = true,
                    TimeWork = 0,
                    WorkingContent = "",
                    CreatedDate = DateTime.Now,
                };
                ViewBag.listUsersCheckIn = listUsersCheckIn;
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                ViewBag.managerCheckOut = currentUser.Id;
                ViewBag.selectedDate = selectedDate;
            //return View();

            return View(tmp2);


                //string json = JsonConvert.SerializeObject(listUsersCheckIn);

                //// Trả về chuỗi JSON trong phản hồi HTTP
                //return Content(json, "application/json");
                ////return View(selectedDate);
            

            //return View();
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
            // Xử lý danh sách id nhận được từ fetch
            // ...
            List<Timesheets> ls = new List<Timesheets>();
            foreach (int i in highlightedCheckout)
            {
                Timesheets tmp = _context.Timesheets.Find(i);

                if (tmp != null)
                {
                    if (tmp.State == "Yes")
                    {
                        tmp.State = "No"; // Sử dụng toán tử gán (=) thay vì toán tử so sánh (==)
                        tmp.UserCheckId = null;
                    }
                    else
                    {
                        tmp.State = "Yes"; // Sử dụng toán tử gán (=) thay vì toán tử so sánh (==)
                        tmp.UserCheckId = idManagerChectOut;
                    }
                }
                _context.Timesheets.Update(tmp);
                _context.SaveChanges();
            }
            //have list time sheet to update yes

            return Json(new { Success = true, Message = "Gửi dữ liệu thành công!" });
            //return Json(data);


            // khong làm như vầy được vì mình gọi js
            //return RedirectToAction("GetUsersTimeKeeping",date);
        }
    }
}
