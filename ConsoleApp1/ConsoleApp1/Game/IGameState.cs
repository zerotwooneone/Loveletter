using System;
using System.Collections.Generic;
using ConsoleApp1.Player;

namespace ConsoleApp1.Game
{
    public interface IGameState
    {
        Guid Id { get; }
        IEnumerable<IGamePlayerState> Players { get; }
    }
}