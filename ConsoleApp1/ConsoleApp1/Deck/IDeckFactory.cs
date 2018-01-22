using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Deck
{
    public interface IDeckFactory
    {
        IEnumerable<IShufflableCardState> Create();
    }
}
