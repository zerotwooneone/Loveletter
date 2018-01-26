using System.Collections.Generic;
using ConsoleApp1.Player;

namespace ConsoleApp1.Game
{
    public interface IGameCreateService
    {
        IInitialGameState CreateGame(IEnumerable<IGamePlayerState> players);
    }
}

