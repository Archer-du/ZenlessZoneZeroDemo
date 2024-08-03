using ZZZDemo.Runtime.Model.Character.Input;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class PlayerController
    {
        private IInputSystemHandler inputSystemHandler;
        public PlayerController(IInputSystemHandler inputSystemHandler)
        {
            this.inputSystemHandler = inputSystemHandler;
        }
        
        public void Update(float deltaTime)
        {
            
        }
    }
}