using Data;
using Logs;
using NaughtyAttributes;
using Network;
using UI;
using UnityEngine;

namespace Core
{
    public class AppManager : Singleton<AppManager>
    {
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isNeedLog;

        private void Start()
        {
            LogManager.Instance.SetIsNeedLog = isNeedLog;

            BoxControllers.OnInit += InitUI;
            SceneManagers.Instance.Init();

            SetKeys();
        }

        private void InitUI()
        {
            BoxControllers.OnInit -= InitUI;

            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();

            NetworkManager.FailedConnectionEvent += FailedConnection;
        }

        private void SetKeys()
        {
            PlayerPrefs.SetString(PrefsKeys.NamePlayer, "Player_" + Random.Range(0, 1000));
            PlayerPrefs.SetString(PrefsKeys.ServerAddress, "127.0.0.1");
            PlayerPrefs.SetInt(PrefsKeys.GameMode, 0);
        }

        public void ConnectToLobby()
        {
            NetworkManager.StartMatch(NetworkMode.Online);
        }

        public void FailedConnection()
        {

        }
    }
}