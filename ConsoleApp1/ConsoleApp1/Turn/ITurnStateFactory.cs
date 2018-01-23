namespace ConsoleApp1.Turn
{
    public interface ITurnStateFactory
    {
        IDrawableTurnState CreateTurn();
    }
}