using MFM.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MFM.BusinessEngine
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationUser _applicationUser;
        public  UserServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IHttpContextAccessor httpContextAccessor )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = httpContextAccessor;
 
        }
        public  ApplicationUser getCurrentUser()
        {
            //var userId = ClaimTypes.NameIdentifier; // will give the current user's userId // in conrtollers only, for other classes use below,
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Task<ApplicationUser> ApplicationUserTask = _userManager.FindByIdAsync(userId);
            return ApplicationUserTask.Result;     
        }
        public string getCurrentUserID()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
