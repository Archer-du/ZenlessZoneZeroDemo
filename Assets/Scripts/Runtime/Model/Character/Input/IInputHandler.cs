using UnityEngine;

namespace ZZZDemo.Runtime.Model.Character.Input
{
    public interface IInputHandler
    {
        public IVirtualJoyStick MoveJoyStick { get; }
        public IVirtualCamera LookAt { get; }
        public IVirtualButton EvadeButton { get; }
        public IVirtualButton LightAttackButton { get; }
    }
    
    public interface IVirtualJoyStick
    {
        public Vector2 Value { get; }
        public Vector2Int Direction { get; }
    }

    public interface IVirtualCamera : IVirtualJoyStick
    {
        public Vector3 GetLookAtDirection();
    }

    public interface IVirtualButton
    {
        public bool Requesting();

        public bool Pressing();

        public void Consume();
    }
}