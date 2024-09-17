using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Behavior.Character.View;
using ZZZDemo.Runtime.Behavior.SO;
using CharacterController = ZZZDemo.Runtime.Model.Character.Controller.CharacterController;

namespace ZZZDemo.Runtime.Behavior.Character
{
    [RequireComponent(typeof(CharacterInputProxy), typeof(CharacterViewProxy))]
    public class CharacterBehavior : MonoBehaviour
    {
        private GlobalParams globalParams;
        
        public CharacterInputProxy inputProxy;
        public CharacterViewProxy viewProxy;
        
        private CharacterController characterController;
#if UNITY_EDITOR
        public CharacterController CharacterController => characterController;
#endif
        private void Awake()
        {
            // config loading phase
            globalParams = GlobalParams.instance;
            if (globalParams == null)
            {
                Debug.LogError("No GlobalParam asset is enabled!");
            }
            globalParams.EnableThis();
            globalParams.ReloadParams();
            
            // construct proxy and controller phase
            inputProxy = GetComponent<CharacterInputProxy>();
            viewProxy = GetComponent<CharacterViewProxy>();
            
            // characterController = new CharacterController(inputProxy, viewProxy);
        }

        void Start()
        {
        }

        void Update()
        {
            characterController.Update(Time.deltaTime);
        }
        
        public void SetController(CharacterController controller) => this.characterController = controller;
    }
}
