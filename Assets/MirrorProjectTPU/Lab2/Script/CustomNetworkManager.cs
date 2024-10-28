using Mirror;

namespace MirrorProjectTPU.Lab2.Script
{
    public class CustomNetworkManager : NetworkManager
    {
        public static CustomNetworkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<CustomNetworkManager>();
                }
                return _instance;
            }
        }
        private static CustomNetworkManager _instance;
        
        public override void OnClientConnect()
        {
            NetworkClient.Ready();
        }

        public void SetupPlayer()
        {
            NetworkClient.AddPlayer();
        }
    }
}
