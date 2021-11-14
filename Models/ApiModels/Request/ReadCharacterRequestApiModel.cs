namespace WebServer.Controllers
{
    public class ReadCharacterRequestApiModel
    {
        public string UserID { get;  set; }
        public string Token { get;  set; }
        public object CharacterId { get;  set; }
        
    }
}