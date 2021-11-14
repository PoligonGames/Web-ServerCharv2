using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Controllers;

namespace WebServer.Services
{
    public interface IEngineManagaer
    {
        IEnumerable<Character> GetPlayerCharacter(string id);
        Task CreateCharacterAsync(string nickName, int gender, string id);
        Task InitCharacterSlotsForNewUserAsync(string email);
        Task DeleteCharacterAsync(object characterToDelete, string id);
        Task<Character> ReadCharacterAsync(object characterId, string id);
    }
}