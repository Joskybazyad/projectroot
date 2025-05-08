using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projectroot.Utilities;
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
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if(!ModelState.IsValid)return View(viewModel);
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (user is not null) 
            {
                bool flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if(flag) 
                {
                    var Result = _signInManager.PasswordSignInAsync(user, viewModel.Password,viewModel.RememberMe,false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                    if(Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked Out");
                    if (Result.Succeeded) return RedirectToAction(nameof(HomeController.Index),"Home");
                }
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(viewModel);
        }
        #endregion
        #region SignOut
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if(user is not null)
                {
                    var Token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = viewModel.Email , Token},Request.Scheme);
                    // create Email
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordUrl // To Do
                    };
                    // send Email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
              
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string Token) 
        {
            TempData["email"]=email;
            TempData["Token"]=Token;
           return View(); 
        }
        
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(!ModelState.IsValid)return View(viewModel);
            string email = TempData["email"] as string ?? string.Empty ;
            string token = TempData["Token"] as string ?? string.Empty ;
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
               var result = _userManager.ResetPasswordAsync(user, token,viewModel.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);
                    }
                }
            }
            return View(nameof(ResetPassword),viewModel);
            
        }

        #endregion
    }
}
