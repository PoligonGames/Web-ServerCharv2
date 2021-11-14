using System.ComponentModel.DataAnnotations;
using WebServer.Models.ApiModels.Request;

namespace WebServer.Controllers
{
    public class CreateCharacterRequestApiModel : RequestBaseApiModel
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        public int Gender { get; set; }
        
       
    }
}