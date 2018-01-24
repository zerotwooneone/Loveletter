namespace ConsoleApp1.Card
{
    public interface ICardDrawService
    {
        IDiscardableCardState Draw(IDrawableCardState card);
        ISetAsideCardState SetAside(IDrawableCardState card);
    }
}