using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.ApiModels.Request;
using WebServer.Services;

namespace WebServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly UserManager<PlayerUser> userManager;
        private readonly IEngineManagaer engineManagaer;

        public CharacterController(UserManager<PlayerUser> userManager, IEngineManagaer engineManagaer)
        {
            this.userManager = userManager;
            this.engineManagaer = engineManagaer;
        }


        [HttpPost]
        public async Task<IEnumerable <Character>> GetSlot([FromBody] RequestBaseApiModel requestApiModel)
        {
            PlayerUser user = await userManager.FindByIdAsync(requestApiModel.UserId);//try to find user bi Id
            Console.WriteLine(requestApiModel.UserId);
            if (user != null && requestApiModel.Token == user.AuthToken)
            {
                return (IEnumerable<Character>)(Character)engineManagaer.GetPlayerCharacter(user.Id);
                
            }
            return null;
        }
        [HttpPost]
        public async Task<ResponseBaseApiModel> Create([FromBody] CreateCharacterRequestApiModel requestApiModel)
            
        {
            PlayerUser user = await userManager.FindByIdAsync(requestApiModel.UserId); // try to find user by Id
            ResponseBaseApiModel responseModel = new CreateCharacterRequestApiModel();

            if (user != null && requestApiModel.Token == user.AuthToken) // if id & token are valid
            {
                await engineManagaer.CreateCharacterAsync(requestApiModel.NickName, requestApiModel.Gender, user.Id);
            }
            if(requestApiModel.NickName != responseModel.NickName)
            {
                responseModel.Status = 8;
                return responseModel;
            }
            else
            {
                responseModel.Status = 8;
                return responseModel;
            }

        }
        [HttpPost]
        public async Task<ReadCharacterResponseApiModel> Read([FromBody] ReadCharacterRequestApiModel requestApiModel)
        {
            PlayerUser user = await userManager.FindByIdAsync(requestApiModel.UserID);
            ReadCharacterResponseApiModel responsModel = new ReadCharacterResponseApiModel();//Create response model

            if(user != null && requestApiModel.Token == user.AuthToken) // If Id & Tokenen are Valid
            {
                Character character = await engineManagaer.ReadCharacterAsync(requestApiModel.CharacterId, user.Id);

                if(character != null) // Null if wrong character Id or user is not character owner
                {
                    responsModel.character = character;
                    responsModel.Status = 0;
                    return responsModel; //Okay
                }
                else
                {
                    responsModel.Status = 7;
                    return responsModel; // Wrong character Id or User is no Character owner
                }
               
            }    
            else
            {
                responsModel.Status = 7;
                return responsModel;// id & tokene are not valid
            }
        }
        [HttpPost]
        public async Task Update([FromBody] RequestBaseApiModel requestApiModel)
        {

        }
        [HttpPost]
        public async Task Delete([FromBody] DeleteCharacterRequestApiModel requestApiModel)
        {
            PlayerUser user = await userManager.FindByIdAsync(requestApiModel.UserID);
            if (user != null && requestApiModel.Token == user.AuthToken)
            {
                await engineManagaer.DeleteCharacterAsync(requestApiModel.CharacterToDelete, user.Id);
            }
        }
    }
}
