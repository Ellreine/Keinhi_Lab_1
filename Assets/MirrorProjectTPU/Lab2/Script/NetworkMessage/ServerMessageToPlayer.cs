
namespace MirrorProjectTPU.Lab2.Script.NetworkMessage
{
    public struct ServerMessageToPlayer : Mirror.NetworkMessage
    {
        public StateHub StateHub;
        public string Nickname;
    }
}
