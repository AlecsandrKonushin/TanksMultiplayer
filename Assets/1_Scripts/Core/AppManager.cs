using Logs;
using NaughtyAttributes;
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
        }

        private void InitUI()
        {
            Debug.Log("Init ui");
            BoxControllers.OnInit -= InitUI;

            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();

            LogManager.Instance.Log("Init UI");
        }

        public void ConnectToLobby()
        {

        }
    }
}