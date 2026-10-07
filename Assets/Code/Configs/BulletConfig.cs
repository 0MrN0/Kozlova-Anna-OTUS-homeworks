using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs / Bullet Config")]
    public sealed class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public Entity Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }
        [field: SerializeField] public float ShotOffset { get; private set; } = 0.5f;
    }
}