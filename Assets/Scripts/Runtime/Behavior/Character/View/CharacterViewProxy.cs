using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using ZZZDemo.Runtime.Model.Character.View.Animation;
using ZZZDemo.Runtime.Model.StateMachine.Character;
using ZZZDemo.Runtime.Model.StateMachine.Character.State;

namespace ZZZDemo.Runtime.Behavior.Character.View
{
    internal class MovementComponent : ICharacterMovement
    {
        private Transform characterTransform;
        public MovementComponent(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }

        public Vector3 GetCharacterForward()
        {
            return characterTransform.forward;
        }

        public void RotateCharacterHorizontal(float angle)
        {
            characterTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * characterTransform.rotation;
        }
    }
    
    internal class AnimationComponent : ICharacterAnimation
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
        internal class IntAnimParam : AnimParamBase, IAnimParamBase<int>
        {
            public IntAnimParam(Animator animator, string name) : base(animator, name){}

            public void Set(int value)
            {
                animator.SetInteger(id, value);
            }
        }
        internal class TriggerAnimParam : AnimParamBase, IAnimParamBase
        {
            public TriggerAnimParam(Animator animator, string name) : base(animator, name){}

            public void Set()
            {
                animator.SetTrigger(id);
            }
        }
        
        
        private Animator animator;
        public IAnimParamBase<bool> Walking => walking;
        private BoolAnimParam walking;
        public IAnimParamBase<bool> Running => running;
        private BoolAnimParam running;
        public IAnimParamBase<float> WalkBlend => walkBlend;
        private FloatAnimParam walkBlend;
        public IAnimParamBase EvadeFront => evadeFront;
        private TriggerAnimParam evadeFront;
        public IAnimParamBase EvadeBack => evadeBack;
        private TriggerAnimParam evadeBack;
        public IAnimParamBase LightAttack => lightAttack;
        private TriggerAnimParam lightAttack;
        public IAnimParamBase<int> LightAttackDeriveLayer => lightAttackDeriveLayer;
        private IntAnimParam lightAttackDeriveLayer;
        
        // TODO: config
        public Dictionary<EAnimationState, int> animStateMap = new()
        {
            { EAnimationState.Walking, Animator.StringToHash("Walk_Blend")},
            { EAnimationState.Running, Animator.StringToHash("Run")},
            { EAnimationState.TurnBack, Animator.StringToHash("TurnBack_NonStop")},
            { EAnimationState.RushAttack, Animator.StringToHash("RushAttack_Start")},
            { EAnimationState.EvadeFront, Animator.StringToHash("EvadeFront_Start")},
            { EAnimationState.EvadeBack, Animator.StringToHash("EvadeBack_Start")},

            { EAnimationState.Anby_DelayAttack, Animator.StringToHash("LightAttackDelay_04_Start") }
        };
        
        public AnimationComponent(Animator animator)
        {
            this.animator = animator;
            walking = new BoolAnimParam(animator, "Walking");
            running = new BoolAnimParam(animator, "Running");
            walkBlend = new FloatAnimParam(animator, "WalkBlend");
            evadeFront = new TriggerAnimParam(animator, "EvadeFront");
            evadeBack = new TriggerAnimParam(animator, "EvadeBack");
            lightAttack = new TriggerAnimParam(animator, "LightAttack");
            lightAttackDeriveLayer = new IntAnimParam(animator, "LightAttackDeriveLayer");
        }
        
        public bool CheckAnimatedRootRotation()
        {
            // TODO: other animated root rotation state
            return CheckTurnBack() || false;
        }

        public bool CheckTurnBack()
        {
            return GetCurrentStateInfo().shortNameHash == animStateMap[EAnimationState.TurnBack];
        }

        public EActionPhase GetActionPhase(EActionType type)
        {
            string typeStr = type.ToString();
            EActionPhase[] phases = (EActionPhase[])Enum.GetValues(typeof(EActionPhase));
            foreach (var phase in phases)
            {
                if (GetCurrentStateInfo().IsTag(typeStr + phase))
                {
                    return phase;
                }
            }
            return EActionPhase.Terminate;
        }

        public void TransitToState(EAnimationState state, float transitionDuration)
        {
            animator.CrossFadeInFixedTime(animStateMap[state], transitionDuration);
        }

        public void TransitToStateNormalized(EAnimationState state, float normalizedDuration)
        {
            animator.CrossFade(animStateMap[state], normalizedDuration);
        }

        private AnimatorStateInfo GetCurrentStateInfo()
        {
            if (animator.IsInTransition(0))
            {
                return animator.GetNextAnimatorStateInfo(0);
            }
            else
            {
                return animator.GetCurrentAnimatorStateInfo(0);
            }
        }

        public EActionPhase actionPhase = EActionPhase.Terminate;
    }
    
    
    [RequireComponent(typeof(Animator))]
    public class CharacterViewProxy : MonoBehaviour, IViewHandler
    {
        public Animator animator;
        public ICharacterMovement Movement => characterMovement;
        private MovementComponent characterMovement;
        public ICharacterAnimation Animation => characterAnimation;
        private AnimationComponent characterAnimation;
        
        public void Awake()
        {
            animator ??= GetComponent<Animator>();
            characterMovement = new MovementComponent(transform);
            characterAnimation = new AnimationComponent(animator);
        }

        private void OnAnimatorMove()
        {
            animator.ApplyBuiltinRootMotion();
        }

        public void OnActionStateEnter()
        {
            characterAnimation.actionPhase = EActionPhase.Startup;
        }
        public void OnActionStateExit()
        {
            characterAnimation.actionPhase = EActionPhase.Recovery;
        }

        #region Debug
        
        public void TestTick()
        {
            // NOTE: 第一帧的animation event和直接获取动画状态有一帧间隔！
            // NOTE: 但是StateMachineBehavior不会！
            var animPhase = characterAnimation.GetActionPhase(EActionType.Attack);
            if(animPhase != characterAnimation.actionPhase)
                Debug.Log(animPhase + "|" + characterAnimation.actionPhase);
        }

        #endregion
    }
}