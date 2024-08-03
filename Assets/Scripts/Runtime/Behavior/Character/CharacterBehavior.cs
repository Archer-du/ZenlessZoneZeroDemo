using System;
using UnityEngine;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Model.Character.Controller;
using ZZZDemo.Runtime.Model.StateMachine;

namespace ZZZDemo.Runtime.Behavior.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterBehavior : MonoBehaviour
    {
        public Animator animator;
        public CharacterController characterController;

        public PlayerController playerController;
        private void Awake()
        {
            playerController = new PlayerController(new InputSystemProxy());
            
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
        }

        void Update()
        {
            playerController.Update(Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
        }
    }

}
