using ZZZDemo.Runtime.Model.Character.Input;

namespace ZZZDemo.Runtime.Behavior.Character.Input
{
    public class InputSystemProxy : IInputSystemHandler
    {
        private InputSystemControl inputSystemControl;
        public InputSystemProxy()
        {
            inputSystemControl = new InputSystemControl();
        }

        public void Enable()
        {
            inputSystemControl.Enable();
        }

        public void Disable()
        {
            inputSystemControl.Disable();
        }
    }
}