using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class BoxManager : MonoBehaviour
    {
        public delegate void Initialize();
        public static event Initialize OnInit;

        private static Dictionary<Type, object> data = new Dictionary<Type, object>();

        private static BaseManager[] managers;

        public static object GetMan { get; internal set; }

        #region INIT

        public static void Init(BaseManager[] managers)
        {
            data.Clear();

            BoxManager.managers = managers;

            Coroutines.StartRoutine(InitGameRoutine());
        }

        private static IEnumerator InitGameRoutine()
        {
            CreateManagers();
            yield return null;

            InitManagers();
            yield return null;

            StartManagers();
            yield return null;
        }

        private static void CreateManagers()
        {
            foreach (var manager in managers)
            {
                var add = Instantiate(manager);

                data.Add(add.GetType(), add);
            }
        }

        private static void InitManagers()
        {
            foreach (var manager in data.Values)
            {
                (manager as BaseManager).OnInitialize();
            }
        }

        private static void StartManagers()
        {
            foreach (var manager in data.Values)
            {
                (manager as BaseManager).OnStart();
            }
        }

        #endregion

        public static T GetManager<T>()
        {
            object manager;
            data.TryGetValue(typeof(T), out manager);
            return (T)manager;
        }
    }
}
