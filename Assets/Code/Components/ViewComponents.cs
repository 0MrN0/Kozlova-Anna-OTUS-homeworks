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

    public struct PrefabComponent
    {
        public Entity Value;
    }
}