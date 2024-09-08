using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Behavior.Character.View;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Behavior.Character
{
    [RequireComponent(typeof(CharacterInputProxy), typeof(CharacterViewProxy))]
    public class CharacterBehavior : MonoBehaviour
    {
        public CharacterInputProxy inputProxy;
        public CharacterViewProxy viewProxy;
        
        private CharacterController characterController;
#if UNITY_EDITOR
        public CharacterController CharacterController => characterController;
#endif
        private void Awake()
        {
            inputProxy = GetComponent<CharacterInputProxy>();
            viewProxy = GetComponent<CharacterViewProxy>();
            
            characterController = new CharacterController(inputProxy, viewProxy);
        }

        void Start()
        {
        }

        void Update()
        {
            // input update phase
            // inputProxy.Update(Time.deltaTime);
            
            // animation update phase
            // viewProxy.Update(Time.deltaTime);

            // core logic update phase
            characterController.Update(Time.deltaTime);
        }

        private void ActionStartup()
        {
            Debug.Log("ActionStartup");
        }
    }

}
