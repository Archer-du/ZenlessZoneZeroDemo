using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Input;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class PlayerController
    {
        private IInputSystemHandler inputSystem;
        public PlayerController(IInputSystemHandler inputSystemHandler)
        {
            this.inputSystem = inputSystemHandler;
        }
        
        public void Update(float deltaTime)
        {
        }
    }
}