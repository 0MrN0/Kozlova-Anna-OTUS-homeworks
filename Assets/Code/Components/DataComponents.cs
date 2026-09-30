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
    public struct PositionRestrictions
    {
        public Vector3 Min;
        public Vector3 Max;
    }

    [Serializable]
    public struct PointerWorldPosition
    {
        public Vector3 Value;
        public bool IsValid;
    }

    [Serializable]
    public struct MoveOffset
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct TurnAngle
    {
        public float Value;
    }
}