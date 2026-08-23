using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM.Editor
{
    [CustomEditor(typeof(LevelHelper))]
    public sealed class LevelHelperEditor : UnityEditor.Editor
    {
        private PlayerData _playerData;
        private int _experience;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            _playerData = (PlayerData)EditorGUILayout.ObjectField("Player Data", _playerData, typeof(PlayerData), false);

            using (new EditorGUI.DisabledScope(!Application.isPlaying || _playerData == null))
            {
                if (GUILayout.Button("Show"))
                {
                    ((LevelHelper)target).Show(_playerData);
                }
            }

            EditorGUILayout.Space();
            _experience = EditorGUILayout.IntField("Experience", _experience);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("AddExperience"))
                {
                    ((LevelHelper)target).AddExperience(_experience);
                }
            }
        }
    }
}
