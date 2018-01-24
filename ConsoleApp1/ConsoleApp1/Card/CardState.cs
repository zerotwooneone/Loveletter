namespace ConsoleApp1.Card
{
    public class CardState : IDrawableCardState, IDiscardableCardState, IDiscardedCardState, IShufflableCardState, ISetAsideCardState
    {
        public int SuffledIndex { get; set; }
        public int Id { get; }

        public CardState(int suffledIndex, int id)
        {
            SuffledIndex = suffledIndex;
            Id = id;
        }
    }
}
