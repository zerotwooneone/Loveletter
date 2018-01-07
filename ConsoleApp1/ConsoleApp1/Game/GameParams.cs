using System.Collections.Generic;
using ConsoleApp1.Player;

namespace ConsoleApp1.Game
{
    public class GameParams
    {
        public IEnumerable<Player.Player> Players { get; set; }
    }
}