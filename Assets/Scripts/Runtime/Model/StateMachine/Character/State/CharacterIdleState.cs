﻿using UnityEngine;
using ZZZDemo.Runtime.Model.Character.Controller;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Model.StateMachine.Character.State
{
    internal class CharacterIdleState : CharacterBaseState
    {
        internal CharacterIdleState(CharacterController controller, CharacterStateMachine stateMachine) : base(controller, stateMachine, ECharacterState.Idle)
        {
        }

        protected override bool CheckTransition()
        {
            if (base.CheckTransition()) return true;
            
            if (controller.IsMoving && controller.IsSharpTurn)
            {
                FSM.ChangeState(ECharacterState.Run);
                return true;
            }
            if (controller.IsMoving && !controller.IsSharpTurn)
            {
                FSM.ChangeState(ECharacterState.Walk);
                return true;
            }
            if (controller.IsEvading)
            {
                FSM.ChangeState(ECharacterState.Evade);
                return true;
            }
            if (controller.IsLightAttacking)
            {
                FSM.ChangeState(ECharacterState.LightAttack);
                return true;
            }
            if (controller.IsHeavyAttacking)
            {
                FSM.ChangeState(ECharacterState.HeavyAttack);
                return true;
            }
            return false;
        }
    }
}