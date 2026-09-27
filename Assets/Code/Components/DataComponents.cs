using System;
using UnityEngine;

namespace Code.Components
{
    [Serializable]
    public struct Position
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }

    [Serializable]
    public struct MoveDirection
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct MoveSpeed
    {
        public float Value;
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