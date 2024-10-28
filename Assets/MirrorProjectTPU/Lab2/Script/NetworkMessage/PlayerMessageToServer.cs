namespace MirrorProjectTPU.Lab2.Script.NetworkMessage
{
    public struct PlayerMessageToServer : Mirror.NetworkMessage
    {
        public StateHub StateHub;
        public string Nickname;
    }
}
