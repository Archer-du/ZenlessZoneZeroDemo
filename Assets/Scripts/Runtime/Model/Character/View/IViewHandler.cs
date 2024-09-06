using UnityEngine;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;

namespace View
{
    public interface IViewHandler
    {
        public ICharacterMovement Movement { get; }
        
        public ICharacterAnimation Animation { get; }
    }

    public interface ICharacterMovement
    {
        // getters
        public Vector3 GetCharacterForward();
        
        //setters
        public void RotateCharacterHorizontal(float angle);
    }

    public interface ICharacterAnimation
    {
        public IAnimParamBase<bool> Walking { get; }
        public IAnimParamBase<bool> Running { get; }
        public IAnimParamBase<float> WalkBlend { get; }
        public IAnimParamBase TurnBack { get; }
        public IAnimParamBase EvadeFront { get; }
        public IAnimParamBase EvadeBack { get; }

        public bool CheckAnimatedRootRotation();
        public bool CheckTurnBack();
        public EEvadePhase GetEvadePhase();
    }
}