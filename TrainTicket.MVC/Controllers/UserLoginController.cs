using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;

namespace TrainTicket.MVC.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserLoginController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult SignIn()
        {
 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
                    if (result.Succeeded)
                    {
                          return RedirectToAction("Index","Admin");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(loginViewModel.Email), "Geçersiz email adresi veya şifresi");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.!");
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Name = registerViewModel.Name;
                user.Id = Guid.NewGuid().ToString();
                user.UserName = "sdfls";
                user.Email = registerViewModel.Email;
                if (registerViewModel.Password == registerViewModel.ConfirmPassword)
                {
                    IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                return View(registerViewModel);
            }

            return View(registerViewModel);
        }


        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }




    }
}
