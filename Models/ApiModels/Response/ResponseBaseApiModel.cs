using System;

namespace WebServer.Controllers
{
    public class ResponseBaseApiModel
    {
        public string NickName { get; set; }
        public object Status { get; set; }

        public static implicit operator ResponseBaseApiModel(CreateCharacterRequestApiModel v)
        {
            return v;
        }
    }
}