using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsAmongUsCMD
{
    // Player wrapper
    [Serializable]
    public class Player
    {
        public String Name { get; set; }
        public Boolean IsDead { get; set; }
        public Boolean IsImposter { get; set; }
        public int Color { get; set; }
        public Vector2 Position { get; set; }


        public void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, IsDead: {IsDead}, IsImposter: {IsImposter}, Color: {Color}, Position: {Position.x},{Position.y}");
        }
    }
}
