using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public interface IDiscardableTurnState : ITurnState
    {
        IDiscardablePlayerState DiscardablePlayer { get; }
        ITargetablePlayerState TargetPlayer { get; set; }
    }
}