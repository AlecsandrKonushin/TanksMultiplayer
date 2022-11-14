using Core;
using Data;
using Logs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static event Action FailedConnectionEvent;

        private int maxPlayers = 12;
        private int onlineSceneIndex = 1;


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
            LogManager.Log($"Not have rooms for join. Create new room.");

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
            short countPlayersInTeams;

            GameMode currentGameMode = ((GameMode)PlayerPrefs.GetInt(PrefsKeys.GameMode));

            switch (currentGameMode)
            {
                case GameMode.DeathMatch:
                    countPlayersInTeams = 2;
                    break;

                default:
                    countPlayersInTeams = 4;
                    break;
            }

            Hashtable roomProperties = new Hashtable();
            roomProperties.Add(RoomExtensions.SIZE, new int[countPlayersInTeams]);
            roomProperties.Add(RoomExtensions.SCORE, new int[countPlayersInTeams]);
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);

            List<int> matchingScenes = new List<int>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string[] scenePath = SceneUtility.GetScenePathByBuildIndex(i).Split('/');

                if (scenePath[scenePath.Length - 1].StartsWith(currentGameMode.ToString()))
                {
                    matchingScenes.Add(i);
                }
            }

            if (matchingScenes.Count == 0)
            {
                LogManager.LogError($"Not have scene for selected. Game mode = {currentGameMode}");
                return;
            }

            onlineSceneIndex = matchingScenes[UnityEngine.Random.Range(0, matchingScenes.Count)];
            PhotonNetwork.LoadLevel(onlineSceneIndex);
        }

        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            if (GameManager.Instance.IsGameOver())
            {
                PhotonNetwork.Disconnect();
                return;
            }

            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            StartCoroutine(CoWaitSceneChange());
        }

        private IEnumerator CoWaitSceneChange()
        {
            while(SceneManager.GetActiveScene().buildIndex != onlineSceneIndex)
            {
                yield return null;
            }

            OnPlayerEnteredRoom(PhotonNetwork.LocalPlayer);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            int teamIndex = GameManager.Instance.GetTeamFill();
            PhotonNetwork.CurrentRoom.AddSize(teamIndex, 1);
            //newPlayer.settaem
        }
    }
}