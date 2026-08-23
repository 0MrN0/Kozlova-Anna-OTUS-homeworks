using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM.Editor
{
    [CustomEditor(typeof(UserHelper))]
    public sealed class UserHelperEditor : UnityEditor.Editor
    {
        private PlayerData _playerData;
        private string _name;
        private string _description;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            _playerData = (PlayerData)EditorGUILayout.ObjectField("Player Data", _playerData, typeof(PlayerData), false);

            using (new EditorGUI.DisabledScope(!Application.isPlaying || _playerData == null))
            {
                if (GUILayout.Button("Show"))
                {
                    var userInfo = new UserInfo(_playerData.Name, _playerData.Description, _playerData.Icon);
                    ((UserHelper)target).Show(userInfo);
                }
            }

            EditorGUILayout.Space();
            _name = EditorGUILayout.TextField("Name", _name);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeName"))
                {
                    ((UserHelper)target).ChangeName(_name);
                }
            }

            EditorGUILayout.Space();
            _description = EditorGUILayout.TextField("Description", _description);

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                if (GUILayout.Button("ChangeDescription"))
                {
                    ((UserHelper)target).ChangeDescription(_description);
                }
            }
        }
    }
}
