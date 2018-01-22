using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public class TurnState: IDrawableTurnState, IDiscardableTurnState
    {
        public ITurnPlayer Player { get; }

        public TurnState(ITurnPlayer player, IDiscardableCardState startingHand)
        {
            Player = player;
            Hand = new []{ startingHand }.ToList();
        }

        public IList<IDiscardableCardState> Hand { get; }
    }
}
