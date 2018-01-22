using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Turn
{
    public interface IDrawableTurnState : ITurnState
    {
        IList<IDiscardableCardState> Hand { get; }
    }
}