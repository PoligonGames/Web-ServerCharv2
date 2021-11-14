using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.ApiModels.Request;
using WebServer.Models.ViewModels;

namespace WebServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {

        private readonly UserManager<PlayerUser> _userManager;
        private readonly SignInManager<PlayerUser> signInManager;

        public AuthController(UserManager<PlayerUser> userManager, SignInManager<PlayerUser> signInManager)
        {

            this._userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            //ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]

        public async Task<LoginRequesApiModel> Login([FromBody] LoginRequesApiModel userModel)
        {
            var result = await signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, false, false);
            PlayerUser user = await _userManager.FindByEmailAsync(userModel.Email);

            LoginRequesApiModel responseModel = new LoginRequesApiModel();
            if (result.Succeeded && !user.IsOnline && !user.IsBanned) //Ok
            {
                user.AuthToken = Guid.NewGuid().ToString(); //Генирируем токен для игрока
                user.IsOnline = true;
                await _userManager.UpdateAsync(user);//обновляем информацыю
                responseModel.Status = 0;
                responseModel.Token = user.AuthToken;
                responseModel.UserID = user.Id;
                responseModel.GamePlayServerIpAddress = "127.0.0.1:7777";

                //Console.WriteLine(user.Email,responseModel.UserID = "is Log in");

                return responseModel;
            }
            else if (result.Succeeded && user.IsOnline && !user.IsBanned)  // user is current logged in
            {
                user.AuthToken = Guid.NewGuid().ToString();
                user.IsOnline = false; //false default
                await _userManager.UpdateAsync(user);
                Console.WriteLine(user.IsOnline);
                responseModel.Status = 2;
                return responseModel;
            }
            else if (result.Succeeded && user.IsBanned) // User is Banned
            {
                responseModel.Status = 3;
                return responseModel;
            }
            else //if (result.Succeeded && user.PasswordHash)
            {
                responseModel.Status = 1; // User incorect password and login
                return responseModel;
            }
        }

        [HttpPost]
        public async Task LogOut([FromBody] RequestBaseApiModel userModel)
        {
            PlayerUser user = await _userManager.FindByIdAsync(userModel.UserId);
            if (user != null && userModel.Token == user.AuthToken)
            {
                user.AuthToken = Guid.NewGuid().ToString();
                user.IsOnline = false;
                await _userManager.UpdateAsync(user);
                Console.WriteLine(user.Email + "is log out");
            }
        }
    }
}