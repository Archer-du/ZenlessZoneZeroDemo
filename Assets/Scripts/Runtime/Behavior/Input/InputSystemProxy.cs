using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Model.Character.Input;

namespace ZZZDemo.Runtime.Behavior.Character.Input
{
    
    public class InputSystemProxy : IInputHandler
    {
        internal struct VirtualJoyStick : IJoyStickHandler
        {
            public Vector2 Value => inputAction.ReadValue<Vector2>().normalized;
            public Vector2Int Direction
            {
                get
                {
                    Vector2 val = inputAction.ReadValue<Vector2>();
                    return new Vector2Int(Math.Sign(val.x), Math.Sign(val.y));
                }
            }

            private readonly InputAction inputAction;
            internal VirtualJoyStick(InputAction action)
            {
                inputAction = action;
            }
        }
        
        internal struct CameraLookAt : IJoyStickHandler, ICameraHandler
        {
            public Vector2 Value => inputAction.ReadValue<Vector2>().normalized;
            public Vector2Int Direction
            {
                get
                {
                    Vector2 val = inputAction.ReadValue<Vector2>();
                    return new Vector2Int(Math.Sign(val.x), Math.Sign(val.y));
                }
            }
            public Vector3 GetLookAtDirection()
            {
                return Camera.main.transform.forward;
            }

            private readonly InputAction inputAction;
            internal CameraLookAt(InputAction action)
            {
                inputAction = action;
            }
        }

        internal struct VirtualButton : IButtonHandler
        {
            internal string Name => inputAction.name;

            private readonly float bufferTime;
            
            private float bufferTimer;
            
            private bool bufferFlag;
            
            public bool Requesting()
            {
                return inputAction.WasPerformedThisFrame() || bufferFlag;
            }
            public void Consume()
            {
                bufferTimer = 0f;
                bufferFlag = false;
            }
            public bool Pressing()
            {
                return inputAction.inProgress;
            }

            private readonly InputAction inputAction;
            internal VirtualButton(InputAction action, float bufferTime)
            {
                inputAction = action;
                this.bufferTime = bufferTime;
                this.bufferTimer = 0f;
                this.bufferFlag = false;
            }

            internal void Update(float deltaTime)
            {
                bufferTimer -= deltaTime;
                if (bufferTimer <= 0)
                {
                    bufferTimer = 0;
                    bufferFlag = false;
                }
                if (inputAction.WasPerformedThisFrame())
                {
                    bufferTimer = bufferTime;
                    bufferFlag = true;
                }
            }
        }

        public IJoyStickHandler MoveJoyStick => moveJoyStick;
        private VirtualJoyStick moveJoyStick;
        public ICameraHandler LookAt => lookJoyStick;
        private CameraLookAt lookJoyStick;
        public IButtonHandler EvadeButton => evadeButton;
        private VirtualButton evadeButton;
        
        
        private InputActionAsset inputActionAsset;
        public InputSystemProxy(InputActionAsset asset)
        {
            inputActionAsset = asset;
            moveJoyStick = new(asset.FindActionMap("Battle").FindAction("Move"));
            lookJoyStick = new(asset.FindActionMap("Battle").FindAction("Look"));
            //TODO: config
            evadeButton = new(asset.FindActionMap("Battle").FindAction("Evade"), 0.2f);
        }

        public void Update(float deltaTime)
        {
            evadeButton.Update(deltaTime);
        }
    }
}