using Code.Components;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs / Camera Config")]
    public sealed class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float LookSensitivity {get; private set;} = 1f;
        [field: SerializeField] public float MinPitch {get; private set;} = -80f;
        [field: SerializeField] public float MaxPitch {get; private set;} = 80f;
        [field: SerializeField] public float MoveSpeed {get; private set;} = 20f;
        [field: SerializeField] public float ZoomSensitivity {get; private set;} = 5f;
        [field: SerializeField] public float MinX {get; private set;} = -75f;
        [field: SerializeField] public float MinY {get; private set;} = 0f;
        [field: SerializeField] public float MinZ {get; private set;} = -75f;
        [field: SerializeField] public float MaxX {get; private set;} = 75f;
        [field: SerializeField] public float MaxY {get; private set;} = 75f;
        [field: SerializeField] public float MaxZ {get; private set;} = 75f;
    }
}