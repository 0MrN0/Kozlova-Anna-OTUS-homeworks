using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs / Spawn Config")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [field: SerializeField] public Entity CubePrefab { get; private set; }
        [field: SerializeField] public float SpawnHeight { get; private set; } = 2f;
    }
}
