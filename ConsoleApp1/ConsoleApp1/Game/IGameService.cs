using System;
using ConsoleApp1.Lobby;

namespace ConsoleApp1.Game
{
    public interface IGameService
    {
        IObservable<LobbyParams> ReturnToLobby { get; }
        IObservable<GameSummary> EndOfGame { get; }
    }
}