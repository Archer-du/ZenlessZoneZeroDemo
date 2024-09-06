using System;
using UnityEngine;

namespace ZZZDemo.Runtime.Behavior.Utils
{
    public class GameSceneDebugManager : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float timeScale;
        private void Update()
        {
            Time.timeScale = timeScale;
        }
    }
}