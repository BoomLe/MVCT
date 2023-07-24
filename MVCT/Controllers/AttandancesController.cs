using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVCT.Data;
using MVCT.Models;
using MVCT.Models.Attandance;
using MVCT.Models.Member;
using MVCT.Repository.ICheckinRepository;

using SkiaSharp;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace MVCT.Controllers
{
    public class AttandancesController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly IAttandancesRepository _repository;

        public AttandancesController(UserManager<AppUser> userManager, ApplicationDbContext db, IAttandancesRepository repository)
        {
            _userManager = userManager;

            _repository = repository;

        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            List<Attandances> objAttList = await _repository.GetAllAsync(includeProperties: "User");
            List<AttandanceInfo> result = new List<AttandanceInfo>();
            var groupByUsers = objAttList.GroupBy(x => new { x.User, x.DataTime.Date });
            foreach (var item in groupByUsers)
            {
                var user = item.Key.User;
                var attendances = item.OrderBy(x => x.DataTime).ToList();

                DateTime? checkIn = null;
                DateTime? checkOut = null;

                if (attendances.Any())
                {
                    checkIn = attendances.First().DataTime;

                    if (attendances.Count > 1)
                    {
                        checkOut = attendances.Last().DataTime;
                    }
                }
                var model = new AttandanceInfo
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Username = user.UserName,
                    CurrentDate = item.Key.Date
                };
                result.Add(model);
            }
            
            return View(result);

        }


    }


    }
    

