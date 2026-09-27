using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs / Camera Config")]
    public sealed class CameraConfig : ScriptableObject
    {
        public float LookSensitivity => _lookSensitivity;
        public float MinPitch => _minPitch;
        public float MaxPitch => _maxPitch;
        public float MoveSpeed => _moveSpeed;
        public float ZoomSensitivity => _zoomSensitivity;

        [SerializeField] private float _lookSensitivity = 1f;
        [SerializeField] private float _minPitch = -80f;
        [SerializeField] private float _maxPitch = 80f;
        [SerializeField] private float _moveSpeed = 20f;
        [SerializeField] private float _zoomSensitivity = 5f;
    }
}