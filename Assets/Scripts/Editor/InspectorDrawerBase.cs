using UnityEditor;
using UnityEngine;

namespace ZZZDemo.Editor
{
    public class InspectorDrawerBase<T> : UnityEditor.Editor where T : MonoBehaviour
    {
        protected T Behavior => target as T;
        
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
        public virtual void DrawInspectorProperties(){}
    }
}