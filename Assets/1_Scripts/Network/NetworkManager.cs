using Core;
using Data;
using ExitGames.Client.Photon;
using Logs;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static event Action FailedConnectionEvent;

        private int maxPlayers = 12;

        public static void StartMatch(NetworkMode mode)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = PlayerPrefs.GetString(PrefsKeys.NamePlayer);

            switch (mode)
            {
                case NetworkMode.Online:
                    PhotonNetwork.ConnectUsingSettings();
                    break;

                case NetworkMode.LAN:
                    PhotonNetwork.ConnectToMaster(PlayerPrefs.GetString(PrefsKeys.ServerAddress), 5055, PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
                    break;

                case NetworkMode.Offline:
                    PhotonNetwork.OfflineMode = true;
                    break;
            }
        }

        public override void OnConnectedToMaster()
        {
            Hashtable roomProperties = new Hashtable() { { NamesData.MODE, (byte)PlayerPrefs.GetInt(PrefsKeys.GameMode) } };

            PhotonNetwork.JoinRandomRoom(roomProperties, (byte)maxPlayers);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            FailedConnectionEvent?.Invoke();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogManager.Instance.Log($"Not have rooms for join. Create new room.");

            RoomOptions roomOptions = new RoomOptions();

            roomOptions.CustomRoomPropertiesForLobby = new string[] { "mode" };
            roomOptions.CustomRoomProperties = new Hashtable() { { NamesData.MODE, (byte)PlayerPrefs.GetInt(PrefsKeys.GameMode) } };
            roomOptions.MaxPlayers = (byte)maxPlayers;
            roomOptions.CleanupCacheOnLeave = false;
            roomOptions.BroadcastPropsChangeToAll = false;

            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            FailedConnectionEvent?.Invoke();
        }

        public override void OnCreatedRoom()
        {
            short initialArrayLength;

            GameMode currentGameMode = ((GameMode)PlayerPrefs.GetInt(PrefsKeys.GameMode));

            switch (currentGameMode)
            {
                case GameMode.DeathMatch:
                    initialArrayLength = 2;
                    break;

                default:
                    initialArrayLength = 4;
                    break;
            }

            Hashtable roomProperties = new Hashtable();
            roomProperties.Add(roomex)
        }
    }
}