using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IPlayerFactory
    {
        IRoundPlayer CreateRoundPlayer(IGamePlayer gameStatePlayer);
        IList<IDiscardedCardState> CreateRoundDiscard();
    }
}