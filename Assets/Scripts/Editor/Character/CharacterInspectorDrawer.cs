using UnityEditor;
using ZZZDemo.Runtime.Behavior.Character;
using ZZZDemo.Runtime.Model.Utils;

namespace ZZZDemo.Editor.Character
{
    [CustomEditor(typeof(CharacterBehavior))]
    public class CharacterInspectorDrawer : InspectorDrawerBase<CharacterBehavior>
    {
        public override void DrawInspectorProperties()
        {
            base.DrawInspectorProperties();

            EditorGUILayout.LabelField("Input");
            EditorGUILayout.Vector2Field("MoveJoyStickValue", Behavior.inputProxy.MoveJoyStick.Value);
            EditorGUILayout.FloatField("MoveJoyStickAngle", MovementUtils.GetRelativeInputAngle(Behavior.inputProxy.MoveJoyStick.Value));
            EditorGUILayout.Space();
        }
    }
}