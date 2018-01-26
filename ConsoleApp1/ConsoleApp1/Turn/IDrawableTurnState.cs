using ConsoleApp1.Card;

namespace ConsoleApp1.Turn
{
    public interface IDrawableTurnState : ITurnState
    {
        IDrawableCardState TurnDeck { get; set; }
    }
}