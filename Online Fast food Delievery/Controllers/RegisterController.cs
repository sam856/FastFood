using FastFoodModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Fast_food_Delievery.Models.Dto;

namespace Online_Fast_food_Delievery.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
             this. signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Name, Email = model.Email , City=model.City  ,
                Address = model.Address , PostalCode = model.PostalCode
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var roleExists = await roleManager.RoleExistsAsync("Customer");
                    if (!roleExists)
                    {
                        var role = new IdentityRole("Customer");
                        await roleManager.CreateAsync(role);
                    }
                    await userManager.AddToRoleAsync(user, "Customer");

                     await signInManager.SignInAsync(user, isPersistent: false);                    
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

    }
}




  
    

