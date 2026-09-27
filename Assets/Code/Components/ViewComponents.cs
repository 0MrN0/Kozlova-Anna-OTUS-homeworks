using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Components
{
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }

    [Serializable]
    public struct Prefab
    {
        public Entity Value;
    }
}