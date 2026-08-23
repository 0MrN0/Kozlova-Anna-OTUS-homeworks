using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM.Editor
{
    [CustomEditor(typeof(PlayerStatsHelper))]
    public sealed class PlayerStatsHelperEditor : UnityEditor.Editor
    {
        private PlayerData _playerData;
        private int _value;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            _playerData = (PlayerData)EditorGUILayout.ObjectField("Player Data", _playerData, typeof(PlayerData), false);

            using (new EditorGUI.DisabledScope(!Application.isPlaying || _playerData == null))
            {
                if (GUILayout.Button("Show"))
                {
                    ((PlayerStatsHelper)target).ShowFirstStat(_playerData);
                }
            }

            EditorGUILayout.Space();
            _value = EditorGUILayout.IntField("Value", _value);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeStatValue"))
                {
                    ((PlayerStatsHelper)target).ChangeFirstStatValue(_value);
                }
            }
        }
    }
}
