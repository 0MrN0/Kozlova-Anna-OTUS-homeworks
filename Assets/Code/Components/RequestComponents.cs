using System;
using UnityEngine;
using Leopotam.EcsLite.Entities;

namespace Code.Components
{
    [Serializable]
    public struct SpawnRequest
    {
        public Entity Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [Serializable]
    public struct TurnRequest
    {

    }

    [Serializable]
    public struct DestroyRequest
    {

    }
}