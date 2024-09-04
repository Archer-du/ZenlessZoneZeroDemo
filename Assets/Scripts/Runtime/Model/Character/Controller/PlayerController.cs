using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class PlayerController
    {
        internal IInputHandler input;
        internal IViewHandler view;

        private CharacterStateMachine characterFSM;
        public PlayerController(IInputHandler inputHandler, IViewHandler viewHandler)
        {
            this.input = inputHandler;
            this.view = viewHandler;

            characterFSM = new CharacterStateMachine();
            characterFSM[ECharacterState.Idle] = new CharacterIdleState(this, characterFSM);
            characterFSM[ECharacterState.Walk] = new CharacterWalkState(this, characterFSM);
            characterFSM[ECharacterState.Run] = new CharacterRunState(this, characterFSM);
            
            characterFSM.Initialize(ECharacterState.Idle);
        }
        
        public void Update(float deltaTime)
        {
            // TODO: why using fsm example
            // if (input.MoveJoyStick.Value != Vector2.zero)
            // {
            //     // smooth rotation
            //     var targetDir = MovementUtils.GetHorizontalProjectionVector(input.LookAt.GetLookAtDirection());
            //     targetDir = MovementUtils.GetRotationByAxis(
            //         MovementUtils.GetRelativeInputAngle(input.MoveJoyStick.Value), Vector3.up) * targetDir;
            //     float angle = MovementUtils.GetRelativeRotateAngle(view.Movement.GetTransformForward(), targetDir);
            //     // TODO: config
            //     const float angleTolerance = 2.5f;
            //     if (Mathf.Abs(angle) > angleTolerance)
            //     {
            //         // TODO: config
            //         const float rotateResponseTime = 0.04f;
            //         view.Movement.RotateCharacterHorizontal(angle * (deltaTime / rotateResponseTime));
            //     }
            //     
            //     // animation
            //     view.Animation.WalkingParam.Set(true);
            // }
            // else
            // {
            //     view.Animation.WalkingParam.Set(false);
            // }
            
            characterFSM.Update(deltaTime);
        }
    }
}