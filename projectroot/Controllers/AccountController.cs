using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectroot.ViewModels.Account;

namespace projectroot.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
		#region Register
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0],
                    Email = viewModel.Email,
                    IsAgree = viewModel.IsAgree,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    

                };
               var result= _userManager.CreateAsync(user,viewModel.Password).Result;
                if(result.Succeeded)return RedirectToAction("Login");
                else
                {
                    foreach(var Error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);
                    }
					return View(viewModel);
				}
                
            }
            return View(viewModel);
        }
        
        #endregion
    }
}
