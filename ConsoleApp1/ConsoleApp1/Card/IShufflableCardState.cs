namespace ConsoleApp1.Card
{
    public interface IShufflableCardState : ICardState
    {
        new int SuffledIndex { get; set; }
    }
}