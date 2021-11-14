using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using WebServer.Models.ViewModels;
using WebServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{

    [Route("api/[controller]")]
    
    public class RegisterController : Controller
    {
 
        private readonly UserManager<PlayerUser> _userManager;
        private readonly SignInManager<PlayerUser> signInManager;
        private readonly IEngineManagaer engineManagaer;

        public RegisterController(UserManager<PlayerUser> userManager,SignInManager<PlayerUser> signInManager,IEngineManagaer engineManagaer)
        {
           
           this._userManager = userManager;
           this.signInManager = signInManager;
           this.engineManagaer = engineManagaer;
        }
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (ModelState.IsValid)
            {

                PlayerUser user = new PlayerUser { UserName = userModel.Email, Email = userModel.Email, IsOnline = false, IsBanned = false };
                var result = await _userManager.CreateAsync(user, userModel.Password);
                
                if (result.Succeeded)
                {
                    await engineManagaer.InitCharacterSlotsForNewUserAsync(user.Email);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
            }
                return View(userModel);
           
            

        }
            
        }
     
    }

