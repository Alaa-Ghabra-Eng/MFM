using JetBrains.Annotations;
using MFM.BusinessEngine;
using MFM.Data;
using MFM.Data.Migrations;
using MFM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using MFM.Models.ViewModels;

namespace MFM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserServices userServices, ApplicationDbContext context)
        {
            _logger = logger;
            UserManager = userManager;
            SignInManager = signInManager;
            _userServices = userServices;
            _context = context;
        }

        public IActionResult Index()
        {   
            UserHomePageViewModel userHomePageViewModel =  new UserHomePageViewModel();
            userHomePageViewModel._totalin = _context.Transactions.Where(x => x.Amount >0).Sum(x => x.Amount);
            userHomePageViewModel._totalout = _context.Transactions.Where(x => x.Amount < 0).Sum(x => x.Amount);
            userHomePageViewModel._MaxIn = _context.Transactions.Where(x=> x.Amount > 0).Max(x => x.Amount);
            userHomePageViewModel._MaxOut = _context.Transactions.Where(x => x.Amount < 0).Max(x => x.Amount);
            ViewData["CurrentUserFirstName"] = _userServices.getCurrentUser().FirstName;      
            return View(userHomePageViewModel);     
        }

        public IActionResult About()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public string GetCurrentUserID()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the current user's userId

            return userId;
        }
  
        public  ApplicationUser GetSessionUser()
        {
            //Task<ApplicationUser> myTask = _userServices.getCurrentUser();
            return _userServices.getCurrentUser();
        }
        public ApplicationUser test() {
            //Task < ApplicationUser > myTask = _userServices.getCurrentUser();
            return _userServices.getCurrentUser();

        
       }
        public string Version() {

           return GetType().Assembly.GetName().Version.ToString();

        }

    }
}
