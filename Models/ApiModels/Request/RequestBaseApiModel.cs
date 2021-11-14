using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models.ApiModels.Request
{
    public class RequestBaseApiModel
    {
        public string UserId { get;  set; }
        public string Token { get;  set; }
    }
}
