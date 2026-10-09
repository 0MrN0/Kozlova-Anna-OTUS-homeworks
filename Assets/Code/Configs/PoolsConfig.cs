using System;
using System.Collections.Generic;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs / Pool Config")]
    public sealed class PoolsConfig : ScriptableObject
    {
        [Serializable]
        public struct PoolEntry
        {
            public Entity Prefab;
            public int Count;
        }

        [field: SerializeField] public List<PoolEntry> Entries { get; private set; } = new();
    }
}