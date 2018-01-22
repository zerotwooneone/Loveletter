namespace ConsoleApp1.Card
{
    public interface IDiscardableCardState : ICardState
    {
        int Id { get; }
    }
}