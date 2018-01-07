namespace ConsoleApp1.Player
{
    public interface IPlayerFactory
    {
        Player CreatePlayer(PlayerParams playerParams);
    }
}