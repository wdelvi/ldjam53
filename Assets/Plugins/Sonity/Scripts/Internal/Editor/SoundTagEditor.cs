// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Sonity.Internal {

    [CustomEditor(typeof(SoundTag))]
    [CanEditMultipleObjects]
    public class SoundTagEditor : Editor {

        public SoundTag mTarget;
        public SoundTag[] mTargets;

        public float pixelsPerIndentLevel = 10f;

        public Color defaultGuiColor;
        public GUIStyle guiStyleBoldCenter = new GUIStyle();

        public void StartBackgroundColor(Color color) {
            GUI.color = color;
            EditorGUILayout.BeginVertical("Button");
            GUI.color = defaultGuiColor;
        }

        public void StopBackgroundColor() {
            EditorGUILayout.EndVertical();
        }

        public override void OnInspectorGUI() {

            mTarget = (SoundTag)target;

            mTargets = new SoundTag[targets.Length];
            for (int i = 0; i < targets.Length; i++) {
                mTargets[i] = (SoundTag)targets[i];
            }

            defaultGuiColor = GUI.color;

            guiStyleBoldCenter.fontSize = 16;
            guiStyleBoldCenter.fontStyle = FontStyle.Bold;
            guiStyleBoldCenter.alignment = TextAnchor.MiddleCenter;
            if (EditorGUIUtility.isProSkin) {
                guiStyleBoldCenter.normal.textColor = EditorColor.GetDarkSkinTextColor();
            }

            EditorGUI.indentLevel = 0;

            StartBackgroundColor(Color.white);
            if (GUILayout.Button(new GUIContent($"Sonity - {nameof(SoundTag)}\n{mTarget.GetName()}", EditorTextSoundTag.soundTagTooltip), guiStyleBoldCenter, GUILayout.ExpandWidth(true), GUILayout.Height(40))) {
                EditorGUIUtility.PingObject(target);
            }
            StopBackgroundColor();
        }
    }
}
#endif