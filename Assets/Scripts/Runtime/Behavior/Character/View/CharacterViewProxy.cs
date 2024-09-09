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
        public IAnimParamBase<int> LightAttackDeriveLayer => lightAttackDeriveLayer;
        private IntAnimParam lightAttackDeriveLayer;
        
        // TODO: config
        public readonly Dictionary<EAnimationState, int> animStateMap = new()
        {
            { EAnimationState.Walking, Animator.StringToHash("Walk_Blend")},
            { EAnimationState.Running, Animator.StringToHash("Run")},
            { EAnimationState.TurnBack, Animator.StringToHash("TurnBack_NonStop")},
            { EAnimationState.RushAttack, Animator.StringToHash("RushAttack_Start")},
            { EAnimationState.LightAttack, Animator.StringToHash("LightAttack_01_Start")},
            { EAnimationState.HeavyAttack, Animator.StringToHash("HeavyAttack")},
            { EAnimationState.EvadeFront, Animator.StringToHash("EvadeFront_Start")},
            { EAnimationState.EvadeBack, Animator.StringToHash("EvadeBack_Start")},

            { EAnimationState.Anby_DelayAttack, Animator.StringToHash("LightAttackDelay_04_Start") },
            { EAnimationState.Anby_DeriveHeavyAttack, Animator.StringToHash("HeavyAttack_Derive") },
        };
        
        public AnimationComponent(Animator animator)
        {
            this.animator = animator;
            walking = new BoolAnimParam(animator, "Walking");
            running = new BoolAnimParam(animator, "Running");
            walkBlend = new FloatAnimParam(animator, "WalkBlend");
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

        /// <summary>
        /// 逆天Unity CrossFade不能指定Interrupt Source，
        /// 因此使用这个接口基本只能转移到ActionState，因为ActionState有Non-Stop阶段（Startup和Active），即便不指定Interrupt Source通常也没问题。
        /// </summary>
        /// <param name="state"></param>
        /// <param name="transitionDuration">必须小于ActionState的Non-Stop阶段持续时间</param>
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
            return animator.GetCurrentAnimatorStateInfo(0);
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
            // var animPhase = characterAnimation.GetActionPhase(EActionType.Attack);
            // if(animPhase != characterAnimation.actionPhase)
            //     Debug.Log(animPhase + "|" + characterAnimation.actionPhase);
            
            var animPhase = characterAnimation.GetActionPhase(EActionType.Attack);
            if (animPhase == EActionPhase.Startup)
            {
               Debug.Log(animPhase);
               if(animator.IsInTransition(0))
                   Debug.Log("Is In Transition");
            }
        }

        #endregion
    }
}