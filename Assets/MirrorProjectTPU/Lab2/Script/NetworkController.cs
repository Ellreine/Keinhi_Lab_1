using Mirror;
using UnityEngine;

namespace MirrorProjectTPU.Lab2.Script
{
    public class NetworkController : MonoBehaviour
    {
        public void ConnectServer()
        {
            NetworkManager.singleton.StartHost();
        }
    }
}
