namespace ConsoleApp1.Game
{
    public interface IGameFactory
    {
        Game CreateGame(GameParams gameParams);
    }
}
