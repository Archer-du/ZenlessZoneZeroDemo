using View;
using ZZZDemo.Runtime.Model.Character.Input;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;
using ZZZDemo.Runtime.Model.StateMachine.Character.State.AnbyDemara;

namespace ZZZDemo.Runtime.Model.Character.Controller
{
    public enum ECharacterType
    {
        AnbyDemara = 0,
        NicoleDemara = 1,
    }
    public class CharacterManager
    {
        public CharacterController AcquireController(ECharacterType characterType, IInputHandler inputHandler,
            IViewHandler viewHandler)
        {
            var controller = new CharacterController(inputHandler, viewHandler);
            switch (characterType)
            {
                case ECharacterType.AnbyDemara:
                    var characterFSM = new CharacterStateMachine();
                    characterFSM[ECharacterState.Idle] = new CharacterIdleState(controller, characterFSM);
                    characterFSM[ECharacterState.Walk] = new CharacterWalkState(controller, characterFSM);
                    characterFSM[ECharacterState.Run] = new CharacterRunState(controller, characterFSM);
                    characterFSM[ECharacterState.Evade] = new CharacterEvadeState(controller, characterFSM);
                    // TODO: generic
                    characterFSM[ECharacterState.LightAttack] = new AnbyDemaraLightAttackState(controller, characterFSM);
                    characterFSM[ECharacterState.HeavyAttack] = new AnbyDemaraHeavyAttackState(controller, characterFSM);
                    characterFSM.Initialize(ECharacterState.Idle);
                    controller.SetControllerFSM(characterFSM);
                    break;
            }
            return controller;
        }
    }
}