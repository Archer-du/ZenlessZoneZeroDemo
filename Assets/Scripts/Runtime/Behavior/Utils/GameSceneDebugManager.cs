using System;
using UnityEngine;

namespace ZZZDemo.Runtime.Behavior.Utils
{
    public class GameSceneDebugManager : MonoBehaviour
    {
        public float timeScale;
        private void Update()
        {
            Time.timeScale = timeScale;
        }
    }
}