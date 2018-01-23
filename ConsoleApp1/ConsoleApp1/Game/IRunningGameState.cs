using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Round;

namespace ConsoleApp1.Game
{
    public interface IRunningGameState : IGameState
    {
        IRunningRoundState RoundState { get; set; }
    }
}