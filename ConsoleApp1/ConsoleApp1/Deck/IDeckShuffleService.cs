using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Deck
{
    public interface IDeckShuffleService
    {
        IEnumerable<IDrawableCardState> Shuffle(IEnumerable<IShufflableCardState> deck);
    }
}