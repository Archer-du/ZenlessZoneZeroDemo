using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ZZZDemo.Runtime.Behavior.Character;

namespace ZZZDemo.Editor.Test
{
    [CustomEditor(typeof(Animator))]
    public class TestAnimatorInspectorDrawer : UnityEditor.Editor
    {
        protected Animator Behavior => target as Animator;
        
        public override bool RequiresConstantRepaint() => true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            if (!EditorApplication.isPlaying) return;

            GUI.enabled = false;
            
            DrawInspectorProperties();

            GUI.enabled = true;
        }

        public void DrawInspectorProperties()
        {
            // Dictionary<int, string> dict = new Dictionary<int, string>
            // {
            //     { Animator.StringToHash("State1"), "State1" },
            //     { Animator.StringToHash("State2"), "State2" },
            //     { Animator.StringToHash("State3"), "State3" },
            //     { Animator.StringToHash("State4"), "State4" },
            // };
            //
            // EditorGUILayout.Toggle("是否在转换", Behavior.IsInTransition(0));
            // EditorGUILayout.TextField("当前状态", dict[Behavior.GetCurrentAnimatorStateInfo(0).shortNameHash]);
            // EditorGUILayout.TextField("下一状态", dict[Behavior.GetNextAnimatorStateInfo(0).shortNameHash]);
        }
    }
}