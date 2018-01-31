namespace ConsoleApp1.Card
{
    public interface ICardStateFactory
    {
        IDiscardableCardState Draw(IDrawableCardState card);
        ISetAsideCardState SetAside(IDrawableCardState card);
        IDiscardedCardState Discard(IDiscardableCardState card);
    }
}