using UnityEngine;
using ZZZDemo.Runtime.Model.Character.View.Animation;

namespace View
{
    public interface IViewHandler
    {
        public IMovementComponent Movement { get; }
        
        public IAnimationComponent Animation { get; }
    }

    public interface IMovementComponent
    {
        // getters
        public Vector3 GetTransformForward();
        
        //setters
        public void RotateCharacterHorizontal(float angle);
    }

    public interface IAnimationComponent
    {
        public IAnimParamBase<bool> WalkingParam { get; }
        public IAnimParamBase<float> WalkBlendParam { get; }
    }
}