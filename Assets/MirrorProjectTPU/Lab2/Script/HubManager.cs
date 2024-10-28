using System;
using System.Collections.Generic;
using Mirror;
using MirrorProjectTPU.Lab2.Script.NetworkMessage;
using UnityEngine;

namespace MirrorProjectTPU.Lab2.Script
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class HubManager : NetworkBehaviour
    {
        private List<string> _listNickname = new();
        private Dictionary<NetworkConnectionToClient, PlayerSetting> _dictPlayer= new();

        private void Start()
        {
            NetworkServer.RegisterHandler<PlayerMessageToServer>(OnPlayerMessageToServer);
        }

        private void OnPlayerMessageToServer(NetworkConnectionToClient conn, PlayerMessageToServer msg)
        {
            if (!_listNickname.Contains(msg.Nickname))
            {
                conn.Send(new ServerMessageToPlayer()
                {
                    StateHub = StateHub.Nickname,
                    Nickname = msg.Nickname
                });
                
                _listNickname.Add(msg.Nickname);
                _dictPlayer.Add(conn, new PlayerSetting(){Nickname = msg.Nickname});
                return;
            }
            conn.Send(new ServerMessageToPlayer()
            {
                StateHub = StateHub.Nickname,
                Nickname = ""
            });
        }
    }
}

public struct PlayerSetting
{
    public string Nickname;
}

public enum StateHub
{
    Nickname
}