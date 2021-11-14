using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.ViewModels;

namespace WebServer.Models.ApiModels.Request
{
    public class LoginRequesApiModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }
        public string UserID { get; set; }
        public string GamePlayServerIpAddress { get; set; }
       
    }
}
