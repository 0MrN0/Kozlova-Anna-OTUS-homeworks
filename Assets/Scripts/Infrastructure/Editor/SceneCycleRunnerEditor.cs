using UnityEditor;
using UnityEngine;

namespace ShootEmUp
{
    [CustomEditor(typeof(SceneCycleRunner))]
    public class SceneCycleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Find MonoBehaviours"))
            {
                var cycleRunner = (SceneCycleRunner)target;
                cycleRunner.SetMonoBehavioursInEditor();
            }
        }
    }
}