using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public class PlayerController
    {
        internal IInputHandler input;
        internal IViewHandler view;

        private CharacterStateMachine characterFSM;
        
        internal Vector2Int lastRunDirection;

        internal float turnbackWindowTimer = 0;
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
            characterFSM.Update(deltaTime);
            UpdateTimer(deltaTime);
        }

        private void UpdateTimer(float deltaTime)
        {
            if (turnbackWindowTimer > 0f)
            {
                turnbackWindowTimer -= deltaTime;
            }
            else
            {
                // invoke timer delegate
                
            }
        }
    }
}