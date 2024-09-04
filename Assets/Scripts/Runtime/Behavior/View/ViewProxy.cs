using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.View.Animation;

namespace ZZZDemo.Runtime.Behavior.View
{
    internal class MovementComponent : IMovementComponent
    {
        private Transform characterTransform;
        public MovementComponent(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }

        public Vector3 GetTransformForward()
        {
            return characterTransform.forward;
        }

        public void RotateCharacterHorizontal(float angle)
        {
            characterTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * characterTransform.rotation;
        }
    }
    internal class AnimationComponent : IAnimationComponent
    {
        internal class AnimParamBase
        {
            protected int id;
            protected Animator animator;
            public AnimParamBase(Animator animator, string name)
            {
                this.animator = animator;
                id = Animator.StringToHash(name);
            }
        }
        internal class BoolAnimParam : AnimParamBase, IAnimParamBase<bool>
        {
            public BoolAnimParam(Animator animator, string name) : base(animator, name){}
            void IAnimParamBase<bool>.Set(bool value)
            {
                animator.SetBool(id, value);
            }
        }
        internal class FloatAnimParam : AnimParamBase, IAnimParamBase<float>
        {
            public FloatAnimParam(Animator animator, string name) : base(animator, name){}

            public void Set(float value)
            {
                animator.SetFloat(id, value);
            }
        }
        private Animator animator;
        public AnimationComponent(Animator animator)
        {
            this.animator = animator;
            walking = new BoolAnimParam(animator, "Walking");
            running = new BoolAnimParam(animator, "Running");
            walkBlend = new FloatAnimParam(animator, "WalkBlend");
        }

        public IAnimParamBase<bool> WalkingParam => walking;
        private BoolAnimParam walking;
        public IAnimParamBase<bool> RunningParam => running;
        private BoolAnimParam running;
        public IAnimParamBase<float> WalkBlendParam => walkBlend;
        private FloatAnimParam walkBlend;
    }
    
    
    public class ViewProxy : IViewHandler
    {
        public ViewProxy(Transform transform, Animator animator)
        {
            movement = new MovementComponent(transform);
            animation = new AnimationComponent(animator);
        }

        public void Update(float deltaTime)
        {
            
        }

        public IMovementComponent Movement => movement;
        private MovementComponent movement;
        public IAnimationComponent Animation => animation;
        private IAnimationComponent animation;
    }
}