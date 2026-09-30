using System;
using UnityEngine;

namespace Code.Components
{
    [Serializable]
    public struct CameraInput
    {
        public Vector2 Look;
        public Vector2 Move;
        public float Zoom;
    }

    [Serializable]
    public struct CameraLook
    {
        public float Yaw;
        public float Pitch;
    }

    [Serializable]
    public struct CameraSettings
    {
        public float LookSensitivity;
        public float ZoomSensitivity;
        public float MinPitch;
        public float MaxPitch;
    }
}