using UnityEditor;
using UnityEngine;
using TooLoo.AI;

namespace TooLoo
{
    [CustomEditor(typeof(AIAction), true)]
    public class AIActionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            AIAction actionSO = (AIAction)target;

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate UID"))
            {
                actionSO.GenerateUID();
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.Space();
        }
    }
}