using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public interface IDrawableTurnState : ITurnState
    {
        IDrawablePlayerState DrawablePlayerState { get; }
        IDrawableCardState TurnDeck { get; set; }
    }
}