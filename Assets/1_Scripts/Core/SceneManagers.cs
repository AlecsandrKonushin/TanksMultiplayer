using Logs;
using UnityEngine;

namespace Core
{
    public class SceneManagers : Singleton<SceneManagers>
    {
        [SerializeField] private BaseManager[] managers;

        public void Init()
        { 
            BoxManager.Init(managers);
        }
    }
}
