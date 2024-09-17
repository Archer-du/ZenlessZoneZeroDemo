using UnityEngine;

namespace ZZZDemo.Runtime.Behavior.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        internal static T Instance { get; private set; }
        
        private static readonly object LockObject = new object();
        
        internal T Get()
        {
            if (Instance == null)
            {
                lock (LockObject)
                {
                    Instance = FindObjectOfType<T>(); 
                    if (Instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        Instance = go.AddComponent<T>();
                    }
                }
            }
            return Instance;
        }
        
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
        }

        protected void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}