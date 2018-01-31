using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Extensions;

namespace ConsoleApp1.Card
{
    public static class Extensions
    {
        public static IDiscardableCardState Draw(this ICardStateFactory cardStateFactory, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.RemoveLast();
            var result = cardStateFactory.Draw(drawableCardState);
            return result;
        }

        public static ISetAsideCardState SetAside(this ICardStateFactory cardStateFactory, IList<IDrawableCardState> deck)
        {
            var drawableCardState = deck.RemoveLast();
            var result = cardStateFactory.SetAside(drawableCardState);
            return result;
        }
    }
}