using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainDbContext mainDb;

        public HomeController(ILogger<HomeController> logger ,MainDbContext mainDb)
        {
            _logger = logger;
            this.mainDb = mainDb;
        }

        public IActionResult Index()
        {


            return View();
            
        }
        [Authorize]
        public async Task<IActionResult> Charater()
        {
            var Characters = await mainDb.Characters.ToListAsync();
            return View(Characters);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<PlayerUser, PlayerUser>()
                    .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            }
            
        }
    }
}
