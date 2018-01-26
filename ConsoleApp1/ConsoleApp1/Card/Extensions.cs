using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Extensions;

namespace ConsoleApp1.Card
{
    public static class Extensions
    {
        public static IDiscardableCardState Draw(this ICardDrawService cardDrawService, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.RemoveLast();
            var result = cardDrawService.Draw(drawableCardState);
            return result;
        }

        public static ISetAsideCardState SetAside(this ICardDrawService cardDrawService, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.RemoveLast();
            var result = cardDrawService.SetAside(drawableCardState);
            return result;
        }
    }
}