using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public class TurnState: IDrawableTurnState, IDiscardableTurnState
    {
        public ITurnPlayerState PlayerState { get; }

        public TurnState(ITurnPlayerState playerState)
        {
            PlayerState = playerState;
        }
    }
}
