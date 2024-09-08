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
        public AnimationComponent(Animator animator)
        {
            this.animator = animator;
            walking = new BoolAnimParam(animator, "Walking");
            running = new BoolAnimParam(animator, "Running");
            walkBlend = new FloatAnimParam(animator, "WalkBlend");
            turnBack = new TriggerAnimParam(animator, "TurnBack");
            evadeFront = new TriggerAnimParam(animator, "EvadeFront");
            evadeBack = new TriggerAnimParam(animator, "EvadeBack");
            lightAttack = new TriggerAnimParam(animator, "LightAttack");
            rushAttack = new TriggerAnimParam(animator, "RushAttack");
            lightAttackDeriveLayer = new IntAnimParam(animator, "LightAttackDeriveLayer");
        }

        public IAnimParamBase<bool> Walking => walking;
        private BoolAnimParam walking;
        public IAnimParamBase<bool> Running => running;
        private BoolAnimParam running;
        public IAnimParamBase<float> WalkBlend => walkBlend;
        private FloatAnimParam walkBlend;
        public IAnimParamBase TurnBack => turnBack;
        private TriggerAnimParam turnBack;
        public IAnimParamBase EvadeFront => evadeFront;
        private TriggerAnimParam evadeFront;
        public IAnimParamBase EvadeBack => evadeBack;
        private TriggerAnimParam evadeBack;
        public IAnimParamBase LightAttack => lightAttack;
        private TriggerAnimParam lightAttack;
        public IAnimParamBase RushAttack => rushAttack;
        private TriggerAnimParam rushAttack;
        public IAnimParamBase<int> LightAttackDeriveLayer => lightAttackDeriveLayer;
        private IntAnimParam lightAttackDeriveLayer;
        
        // TODO:
        public bool CheckAnimatedRootRotation()
        {
            if (animator.IsInTransition(0))
            {
                return animator.GetNextAnimatorStateInfo(0).shortNameHash ==
                       Animator.StringToHash("TurnBack_NonStop");
            }
            return animator.GetCurrentAnimatorStateInfo(0).shortNameHash ==
                   Animator.StringToHash("TurnBack_NonStop");
        }

        public bool CheckTurnBack()
        {
            if (animator.IsInTransition(0))
            {
                return animator.GetNextAnimatorStateInfo(0).shortNameHash ==
                       Animator.StringToHash("TurnBack_NonStop");
            }
            return animator.GetCurrentAnimatorStateInfo(0).shortNameHash ==
                   Animator.StringToHash("TurnBack_NonStop");
        }

        public EActionPhase GetActionPhase(EActionType type)
        {
            string typeStr = type.ToString();
            List<EActionPhase> phases = new List<EActionPhase>()
                { EActionPhase.Start, EActionPhase.Active, EActionPhase.Derive, EActionPhase.Recovery };
            foreach (var phase in phases)
            {
                if (animator.IsInTransition(0))
                {
                    if (animator.GetNextAnimatorStateInfo(0).IsTag(typeStr + phase))
                        return phase;
                }
                else
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsTag(typeStr + phase))
                        return phase;
                }
            }
            return EActionPhase.Terminate;
        }
    }
    [RequireComponent(typeof(Animator))]
    public class CharacterViewProxy :MonoBehaviour, IViewHandler
    {
        public Animator animator;
        public ICharacterMovement Movement => characterMovement;
        private MovementComponent characterMovement;
        public ICharacterAnimation Animation => characterAnimation;
        private ICharacterAnimation characterAnimation;
        
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
    }
}