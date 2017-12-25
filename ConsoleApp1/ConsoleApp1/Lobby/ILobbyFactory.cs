namespace ConsoleApp1.Lobby
{
    public interface ILobbyFactory
    {
        Lobby CreateLobby(LobbyParams lobbyParams = null);
    }
}
