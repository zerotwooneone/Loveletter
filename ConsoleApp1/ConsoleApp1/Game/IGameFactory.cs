namespace ConsoleApp1.Game
{
    public interface IGameFactory
    {
        IInitialGameState CreateGame();
    }
}
