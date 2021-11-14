using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Controllers;
using WebServer.Services;

namespace WebServer
{
    public class EngineManager : IEngineManagaer

    {
        private readonly MainDbContext db;

        public EngineManager(MainDbContext db)
        {
            this.db = db;
        }

        public async Task CreateCharacterAsync(string nickName, int gender, string userId)//Read.It s call when create new character
        {
            Character slotToCreate = await db.Characters.Where(c => c.OwnerId == userId && c.IsCreated == false).FirstOrDefaultAsync();
            if (slotToCreate == null)
            {
                return; // Have on emty slots for the new Character
            }
            slotToCreate.IsCreated = true;
            slotToCreate.Nickname = nickName;
            slotToCreate.Gender = gender;
            slotToCreate.Experiance = 0;
            await db.SaveChangesAsync();
        }

        public Task DeleteCharacterAsync(object characterToDelete, string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Character> GetPlayerCharacter(string userId)
        {
            return db.Characters.Where(c => c.OwnerId == userId).ToList();
        }

        public async Task InitCharacterSlotsForNewUserAsync(string email)
        {
            var newUser = await db.PlayerUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            List<Character> newSlot = new List<Character>
            {
                new Character { IsCreated = false, OwnerId = newUser.Id },
               // new Character { IsCreated = false, OwnerId = newUser.Id },
                //new Character { IsCreated = false, OwnerId = newUser.Id }
            };
            await db.Characters.AddRangeAsync(newSlot);
            await db.SaveChangesAsync();
        }

        public Task<Character> ReadCharacterAsync(object characterId, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}