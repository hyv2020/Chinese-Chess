
namespace GameClient
{
    public interface IClientObserver
    {
        void OnTcpDataReceived(object data);

    }
}
