using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using ZZZDemo.Runtime.Behavior.Character;
using ZZZDemo.Runtime.Behavior.Character.Input;
using ZZZDemo.Runtime.Behavior.Character.View;
using ZZZDemo.Runtime.Behavior.Utils;
using ZZZDemo.Runtime.Model.Character.Controller;

namespace ZZZDemo.Runtime.Behavior
{
    public class GameManager : MonoSingleton<GameManager>
    {
        // TODO: config
        public CinemachineVirtualCamera TPPCamera;
        public ECharacterType initialCharacter = ECharacterType.AnbyDemara;
        public List<GameObject> prototypes = new();
        
        private Dictionary<ECharacterType, CharacterBehavior> behaviors = new();
        private CharacterManager characterManager = new();
        protected override void Awake()
        {
            for (int i = 0; i < prototypes.Count; i++)
            {
                var go = Instantiate(prototypes[i], Vector3.zero, Quaternion.identity);
                behaviors.Add((ECharacterType)i, go.GetComponent<CharacterBehavior>());
                if ((ECharacterType)i != initialCharacter)
                {
                    go.SetActive(false);
                }
                var inputProxy = behaviors[(ECharacterType)i].GetComponent<CharacterInputProxy>();
                var viewProxy = behaviors[(ECharacterType)i].GetComponent<CharacterViewProxy>();
                var controller = characterManager.AcquireController((ECharacterType)i, inputProxy, viewProxy);
                behaviors[(ECharacterType)i].SetController(controller);
            }
            SetCameraFocusOn(behaviors[initialCharacter]);
        }

        private void SetCameraFocusOn(CharacterBehavior behavior)
        {
            behavior.TryGetComponent<CharacterViewProxy>(out var viewProxy);
            if (viewProxy)
            {
                TPPCamera.Follow = viewProxy.cameraFollow;
                TPPCamera.LookAt = viewProxy.cameraLookAt;
            }
        }
    }
}