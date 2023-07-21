using Book.Unitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCT.Data;
using MVCT.Models.Attandance;
using SkiaSharp;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace MVCT.Controllers
{
    public class AttandancesController : Controller
    {

        private readonly IUnitityOfWork _unitityOfWork;

        public AttandancesController(IUnitityOfWork unitityOfWork)
        {
            _unitityOfWork = unitityOfWork;
        }

        public async Task<IActionResult> Index() 
        {
            List<Attandances> listCategories = _unitityOfWork.AttandancesRepo.GetAll().ToList();
            return View(listCategories);
        }
    }
}
