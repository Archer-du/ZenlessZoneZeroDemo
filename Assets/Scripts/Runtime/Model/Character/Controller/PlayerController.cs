using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class PlayerController
    {
        private IInputHandler input;
        private IViewHandler view;
        public PlayerController(IInputHandler inputHandler, IViewHandler viewHandler)
        {
            this.input = inputHandler;
            this.view = viewHandler;
        }
        
        public void Update(float deltaTime)
        {
            if (input.MoveJoyStick.Value != Vector2.zero)
            {
                var targetDir = MovementUtils.GetXZProjection(input.LookAt.GetLookAtDirection());
                
            }
        }
    }
}