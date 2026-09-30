using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs / Camera Config")]
    public sealed class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float LookSensitivity { get; private set; } = 1f;
        [field: SerializeField] public float MinPitch { get; private set; } = -80f;
        [field: SerializeField] public float MaxPitch { get; private set; } = 80f;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 20f;
        [field: SerializeField] public float ZoomSensitivity { get; private set; } = 5f;
        [field: SerializeField] public Vector3 MinPosition { get; private set; } = new(-35, 5, -60);
        [field: SerializeField] public Vector3 MaxPosition { get; private set; } = new(35, 100, -5);
    }
}