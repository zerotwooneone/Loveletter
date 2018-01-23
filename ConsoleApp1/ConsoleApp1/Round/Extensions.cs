using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Round
{
    public static class Extensions
    {
        public static IEnumerable<IDrawableCardState> RemoveFromDeck(this IDeckRemovalService deckRemovalService,
            IList<IDrawableCardState> deck,
            int playerCount)
        {
            var cardCount = deckRemovalService.GetCardsToRemoveCount(playerCount);
            var removed = deckRemovalService.RemoveFromDeck(deck, cardCount);
            return removed;
        }
    }
}