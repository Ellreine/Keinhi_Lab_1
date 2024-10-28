using System;
using Mirror;
using MirrorProjectTPU.Lab2.Script.NetworkMessage;
using UnityEngine;

namespace MirrorProjectTPU.Lab2.Script
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class HubController : NetworkBehaviour
    {
        [SerializeField] private InputNicknameWindowConrroller inputNicknameWindowConrroller;
        [SerializeField] private PlayWindowConrtoller playWindowConrtoller;

        public void SendNickname()
        {
            if (String.IsNullOrWhiteSpace(inputNicknameWindowConrroller.InputNickname.text))
                return;
            NetworkClient.Send(new PlayerMessageToServer()
            {
                StateHub = StateHub.Nickname,
                Nickname = inputNicknameWindowConrroller.InputNickname.text
            });
        }
        
        private void Start()
        {
            NetworkClient.RegisterHandler<ServerMessageToPlayer>(OnServerMessageToPlayer);
        }

        private void OnServerMessageToPlayer(ServerMessageToPlayer msg)
        {
            switch (msg.StateHub)
            {
                case StateHub.Nickname:
                    CheckNickname(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckNickname(ServerMessageToPlayer msg)
        {
            if (String.IsNullOrEmpty(msg.Nickname))
            {
                inputNicknameWindowConrroller.ErrorNickname();
                return;
            }
            inputNicknameWindowConrroller.gameObject.SetActive(false);
            playWindowConrtoller.gameObject.SetActive(true);
            playWindowConrtoller.SetNickname(msg.Nickname);
            CustomNetworkManager.Instance.SetupPlayer();
        }
    }
}
