using System;
using ConsoleApp1.Game;

namespace ConsoleApp1.Lobby
{
    public interface ILobbyService
    {
        IObservable<GameParams> StartGame { get; }
    }
}