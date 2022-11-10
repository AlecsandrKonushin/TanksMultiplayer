using UnityEngine;

namespace Core
{
    public class BaseController : ScriptableObject, IController
    {
        public virtual void OnInitialize() { }

        public virtual void OnStart() { }
    }
}