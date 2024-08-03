using UnityEngine;

namespace ZZZDemo.Runtime.Model.Character.Input
{
    public interface IInputSystemHandler
    {
        public IVirtualJoyStickHandler MoveHandler { get; }
        
        public IVirtualButtonHandler EvadeHandler { get; }
    }

    public interface IVirtualJoyStickHandler
    {
        public Vector2 Value { get; }
        public Vector2Int Direction { get; }
    }

    public interface IVirtualButtonHandler
    {
        public bool Requesting();

        public bool Pressing();

        public void Consume();
    }
}