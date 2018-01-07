using System.Collections.Generic;
using ConsoleApp1.Player;

namespace ConsoleApp1.Lobby
{
    public class LobbyParams
    {
        public IEnumerable<PlayerParams> Players { get; set; }
    }
}