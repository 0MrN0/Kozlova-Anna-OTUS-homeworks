using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "Configs / Cube Config")]
    public sealed class CubeConfig : ScriptableObject
    {
        [field: SerializeField] public Entity RedCubePrefab { get; private set; }
        [field: SerializeField] public Entity BlueCubePrefab { get; private set; }
        [field: SerializeField] public float SpawnHeight { get; private set; } = 2f;
    }
}
