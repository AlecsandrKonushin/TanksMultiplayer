using Logs;
using NaughtyAttributes;
using System.Collections;
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

            BoxManager.OnInit += InitUI;
            SceneManagers.Instance.Init();
        }

        private void InitUI()
        {
            Debug.Log("Init ui");
            BoxManager.OnInit -= InitUI;

            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();

            LogManager.Instance.Log("Init UI");
        }
    }
}