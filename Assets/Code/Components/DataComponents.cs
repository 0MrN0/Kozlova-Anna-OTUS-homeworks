using System;
using Leopotam.EcsLite;
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

    [Serializable]
    public struct Damage
    {
        public int Value;
    }

    [Serializable]
    public struct Lifetime
    {
        public float Value;
    }

    [Serializable]
    public struct VisionRadius
    {
        public float Value;
    }

    [Serializable]
    public struct AttackTarget
    {
        public EcsPackedEntity Value;
    }

    [Serializable]
    public struct AttackCooldown
    {
        public float Duration;
        public float Timer;
    }

    [Serializable]
    public struct Health
    {
        public int Max;
        public int Current;
    }

    [Serializable]
    public struct DeathTimer
    {
        public float Value;
    }

    [Serializable]
    public struct DeathDelay
    {
        public float Value;
    }
}