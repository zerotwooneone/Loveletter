using System;
using System.Collections.Generic;

namespace ConsoleApp1.Game
{
    public interface IGameService
    {
        //IObservable<LobbyParams> ReturnToLobby { get; }
        //IObservable<GameSummary> EndOfGame { get; }
        GameParams GetGameParams(IEnumerable<Player.Player> players);
    }
}