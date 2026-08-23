using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM.Editor
{
    [CustomEditor(typeof(PlayerStatsHelper))]
    public sealed class PlayerStatsHelperEditor : UnityEditor.Editor
    {
        private PlayerData _playerData;
        private string _statName;
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
                    var playerStats = new PlayerStats(_playerData.Stats);
                    ((PlayerStatsHelper)target).Show(playerStats);
                }
            }

            EditorGUILayout.Space();
            _statName = EditorGUILayout.TextField("Stat Name", _statName);
            _value = EditorGUILayout.IntField("Value", _value);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeStatValue"))
                {
                    ((PlayerStatsHelper)target).ChangeStat(_statName, _value);
                }
            }
        }
    }
}
