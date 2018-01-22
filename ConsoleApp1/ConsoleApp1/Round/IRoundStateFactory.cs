using System.Collections.Generic;
using ConsoleApp1.Game;

namespace ConsoleApp1.Round
{
    public interface IRoundStateFactory
    {
        RoundState Create(int roundIndex,
            IEnumerable<Player.Player> players);
    }
}