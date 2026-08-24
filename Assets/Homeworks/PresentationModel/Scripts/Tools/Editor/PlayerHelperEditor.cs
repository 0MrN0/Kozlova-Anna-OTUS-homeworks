using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM.Editor
{
    [CustomEditor(typeof(PlayerHelper))]
    public sealed class PlayerHelperEditor : UnityEditor.Editor
    {
        private int _experience;
        private string _statName;
        private int _value;
        private string _name;
        private string _description;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("Show"))
                {
                    ((PlayerHelper)target).Show();
                }
            }

            EditorGUILayout.Space();
            _experience = EditorGUILayout.IntField("Experience", _experience);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("AddExperience"))
                {
                    ((PlayerHelper)target).AddExperience(_experience);
                }
            }

            EditorGUILayout.Space();
            _statName = EditorGUILayout.TextField("Stat Name", _statName);
            _value = EditorGUILayout.IntField("Value", _value);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeStatValue"))
                {
                    ((PlayerHelper)target).ChangeStat(_statName, _value);
                }
            }

            EditorGUILayout.Space();
            _name = EditorGUILayout.TextField("Name", _name);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeName"))
                {
                    ((PlayerHelper)target).ChangeName(_name);
                }
            }

            EditorGUILayout.Space();
            _description = EditorGUILayout.TextField("Description", _description);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeDescription"))
                {
                    ((PlayerHelper)target).ChangeDescription(_description);
                }
            }
        }
    }
}
