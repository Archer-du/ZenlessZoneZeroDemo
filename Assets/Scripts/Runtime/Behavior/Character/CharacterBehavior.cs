using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine;

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
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();

            inputSystemProxy = new InputSystemProxy(playerInput.actions);
            playerController = new PlayerController(inputSystemProxy);
        }

        void Start()
        {
        }

        void Update()
        {
            // input update phase
            inputSystemProxy.Update(Time.deltaTime);
            
            // core logic update phase
            playerController.Update(Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            // apply root motion
            transform.position += animator.deltaPosition;
            transform.rotation *= animator.deltaRotation;
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }

}
