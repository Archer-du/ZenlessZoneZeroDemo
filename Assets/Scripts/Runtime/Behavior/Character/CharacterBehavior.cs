using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Behavior.View;
using ZZZDemo.Runtime.Behavior.Character.Input;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Behavior.Character
{
    [RequireComponent(typeof(Animator), typeof(PlayerInput))]
    public class CharacterBehavior : MonoBehaviour
    {
        public Animator animator;
        public PlayerInput playerInput;
        
        private CharacterController characterController;

        private InputSystemProxy inputSystemProxy;
        private ViewProxy viewProxy;
        
#if UNITY_EDITOR
        public CharacterController CharacterController => characterController;
        public InputSystemProxy InputSystemProxy => inputSystemProxy;
        private ViewProxy ViewProxy => viewProxy;
#endif
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();

            inputSystemProxy = new InputSystemProxy(playerInput.actions);
            viewProxy = new ViewProxy(transform, animator);
            
            characterController = new CharacterController(inputSystemProxy, viewProxy);
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
            characterController.Update(Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
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
