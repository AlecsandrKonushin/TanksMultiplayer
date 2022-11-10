using Logs;
using UnityEngine;

namespace Core
{
    public class SceneManagers : Singleton<SceneManagers>
    {
        [SerializeField] private BaseController[] managers;

        public void Init()
        { 
            BoxControllers.Init(managers);
        }
    }
}
