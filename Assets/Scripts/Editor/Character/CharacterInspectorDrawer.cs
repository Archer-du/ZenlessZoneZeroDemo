using UnityEditor;
using ZZZDemo.Runtime.Behavior.Character;

namespace ZZZDemo.Editor.Character
{
    [CustomEditor(typeof(CharacterBehavior))]
    public class CharacterInspectorDrawer : InspectorDrawerBase<CharacterBehavior>
    {
        public override void DrawInspectorProperties()
        {
            base.DrawInspectorProperties();

            EditorGUILayout.LabelField("Input");
            EditorGUILayout.Space();
        }
    }
}