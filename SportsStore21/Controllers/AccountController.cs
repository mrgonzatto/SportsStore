using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore21.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore21.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
                                 SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login( string returnUrl )
        {
            return View( new LoginModel { ReturnUrl = returnUrl } );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginModel loginModel )
        {
            if (ModelState.IsValid)
            {
                IdentityUser user =
                    await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if (
                        (await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded
                    ) {
                        return Redirect( loginModel?.ReturnUrl ?? "Admin/Index" );
                    }
                }
            }

            ModelState.AddModelError("", "Usuário ou senha inválidos!");
            return View(loginModel);

        }

        public async Task<RedirectResult> Logout( string returnUrl = "/" ) 
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}
