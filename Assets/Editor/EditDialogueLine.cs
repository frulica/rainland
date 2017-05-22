using UnityEditor;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
    [CustomEditor(typeof(DialogueLine)), CanEditMultipleObjects]
    public class EditDialogueLine : Editor {

        public SerializedProperty longStringProp;
        void OnEnable () {
            longStringProp = serializedObject.FindProperty ("text");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update ();
            longStringProp.stringValue = EditorGUILayout.TextArea( longStringProp.stringValue, GUILayout.MaxHeight(75) );
            serializedObject.ApplyModifiedProperties ();
        }
    }
}