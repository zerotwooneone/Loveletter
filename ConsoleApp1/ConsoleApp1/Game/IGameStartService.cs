namespace ConsoleApp1.Game
{
    public interface IGameStartService
    {
        IRunningGameState StartGame(IInitialGameState gameState);
    }
}