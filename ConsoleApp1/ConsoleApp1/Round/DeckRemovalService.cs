using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;

namespace ConsoleApp1.Round
{
    public class DeckRemovalService : IDeckRemovalService
    {
        public int GetCardsToRemoveCount(int playerCount)
        {
            int result;
            switch (playerCount)
            {
                case 2:
                    result = 4;
                    break;
                case 3:
                case 4:
                    result = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid number of players");
            }
            return result;
        }

        public IEnumerable<IDrawableCardState> RemoveFromDeck(IList<IDrawableCardState> deck, int countToRemove)
        {
            var result = deck.Skip(countToRemove);
            return result;
        }
    }
}