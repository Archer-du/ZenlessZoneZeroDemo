using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestAnimFSM : StateMachineBehaviour
    {
        Dictionary<int, string> dict = new Dictionary<int, string>
        {
            { Animator.StringToHash("State1"), "State1" },
            { Animator.StringToHash("State2"), "State2" },
            { Animator.StringToHash("State3"), "State3" },
            { Animator.StringToHash("State4"), "State4" },
        };
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Debug.Log(stateInfo);
            Debug.Log("Entered: " + dict[stateInfo.shortNameHash]);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Debug.Log(stateInfo);
            Debug.Log("Exit: " + dict[stateInfo.shortNameHash]);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (stateInfo.IsName("State1"))
            {
                Debug.Log("Updating state1");
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}