using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs / Spawn Config")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [field: SerializeField] public Entity CubePrefab { get; private set; }
        [field: SerializeField] public float SpawnHeight { get; private set; } = 2f;

        [field: Header("Start Formation")]
        [field: SerializeField] public int StartCountPerTeam { get; private set; } = 100;
        [field: SerializeField] public int StartColumns { get; private set; } = 20;
        [field: SerializeField] public float StartSpacing { get; private set; } = 2f;
        [field: SerializeField] public float StartEdgeOffset { get; private set; } = 5f;
    }
}
