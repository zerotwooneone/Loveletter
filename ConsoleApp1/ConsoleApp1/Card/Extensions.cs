using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Card
{
    public static class Extensions
    {
        public static IDiscardableCardState Draw(this ICardDrawService cardDrawService, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.Last();
            deck.Remove(drawableCardState);
            var result = cardDrawService.Draw(drawableCardState);
            return result;
        }

        public static ISetAsideCardState SetAside(this ICardDrawService cardDrawService, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.Last();
            deck.Remove(drawableCardState);
            var result = cardDrawService.SetAside(drawableCardState);
            return result;
        }
    }
}