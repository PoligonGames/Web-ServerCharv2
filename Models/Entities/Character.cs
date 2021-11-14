using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebServer
{
    public class Character
    {
        
        public int Id { get; set; }
         
        public string OwnerId { get; set; }
        public string Nickname { get; set; }
        public int Gender { get; set; } // 0 - Male, 1 - Female
        public int Experiance { get; set; } = 0;
        public float LocationX { get; set; } = 0.0f;
        public float LocationY { get; set; } = 0.0f;
        public float LocationZ { get; set; } = 0.0f;
        public float RotationX { get; set; } = 1.0f;
        public float RotationY { get; set; } = 1.0f;
        public float RotationZ { get; set; } = 1.0f;
        public bool IsCreated { get; set; }
        //public IEnumerable<PlayerUser> user { get; set; }
    }
}