using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Round
{
    public interface IDeckRemovalService
    {
        int GetCardsToRemoveCount(int playerCount);
        IEnumerable<IDrawableCardState> RemoveFromDeck(IList<IDrawableCardState> deck, int countToRemove);
    }
}