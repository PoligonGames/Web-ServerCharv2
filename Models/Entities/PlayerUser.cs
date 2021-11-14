using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebServer
{
    public class PlayerUser : IdentityUser
    {
        
        public string AuthToken { get; set; }
        public bool IsOnline { get; set; } = false;
        public bool MustBeLogOuted { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        //public Character user { get; set; }

    }
}