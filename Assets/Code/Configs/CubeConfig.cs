using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "Configs / Cube Config")]
    public sealed class CubeConfig : ScriptableObject
    {
        [field: SerializeField] public Entity CubePrefab { get; private set; }
        [field: SerializeField] public float SpawnHeight { get; private set; } = 2f;
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float TurnAngle { get; private set; } = -135f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 0.5f;
        [field: SerializeField] public int Health { get; private set; } = 5;
    }
}
