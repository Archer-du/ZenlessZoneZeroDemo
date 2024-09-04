using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Behavior.View;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Runtime.Behavior.Character
{
    [RequireComponent(typeof(Animator), typeof(PlayerInput), typeof(CharacterController))]
    public class CharacterBehavior : MonoBehaviour
    {
        public Animator animator;
        public PlayerInput playerInput;
        public CharacterController characterController;
        
        private PlayerController playerController;

        private InputSystemProxy inputSystemProxy;
        private ViewProxy viewProxy;
        
#if UNITY_EDITOR
        public PlayerController PlayerController => playerController;
        public InputSystemProxy InputSystemProxy => inputSystemProxy;
        private ViewProxy ViewProxy => viewProxy;
#endif
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();

            inputSystemProxy = new InputSystemProxy(playerInput.actions);
            viewProxy = new ViewProxy(transform, animator);
            
            playerController = new PlayerController(inputSystemProxy, viewProxy);
        }

        void Start()
        {
        }

        void Update()
        {
            // input update phase
            inputSystemProxy.Update(Time.deltaTime);
            
            // animation update phase
            viewProxy.Update(Time.deltaTime);
            
            // core logic update phase
            playerController.Update(Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            // apply root motion
            // transform.position += animator.deltaPosition;
            // transform.rotation *= animator.deltaRotation;
            animator.ApplyBuiltinRootMotion();
        }

        private void OnEnable()
        {
            playerInput.actions.Enable();
        }

        private void OnDisable()
        {
            playerInput.actions.Enable();
        }
    }

}
