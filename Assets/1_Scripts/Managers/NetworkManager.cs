using Data;
using Photon.Pun;
using System;
using UnityEngine;

namespace Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static event Action FailedConnectionEvent;

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
    }
}