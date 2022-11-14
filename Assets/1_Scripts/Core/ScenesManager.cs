using Logs;
using UnityEngine;

namespace Core
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        [SerializeField] private BaseController[] managers;

        public void Init()
        { 
            BoxControllers.Init(managers);
        }
    }
}
