using UnityEngine;

namespace ZZZDemo.Runtime.Model.Character.Input
{
    public interface IInputHandler
    {
        public IJoyStickHandler MoveJoyStick { get; }
        public ICameraHandler LookAt { get; }
        public IButtonHandler EvadeButton { get; }
    }
    
    public interface IJoyStickHandler
    {
        public Vector2 Value { get; }
        public Vector2Int Direction { get; }
    }

    public interface ICameraHandler : IJoyStickHandler
    {
        public Vector3 GetLookAtDirection();
    }

    public interface IButtonHandler
    {
        public bool Requesting();

        public bool Pressing();

        public void Consume();
    }
}