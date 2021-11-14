namespace WebServer.Controllers
{
    public class DeleteCharacterRequestApiModel
    {
        public string Token { get; set; }
        public string UserID { get; set; }
        public bool CharacterToDelete { get; set; }
    }
}