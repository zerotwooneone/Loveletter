using System;
using System.Collections.Generic;

namespace ConsoleApp1.Game
{
    public interface IGameStateFactory
    {
        GameState Create(Guid Id, IEnumerable<Player.Player> players);
    }
}