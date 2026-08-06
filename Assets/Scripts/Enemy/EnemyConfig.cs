using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Enemies/New EnemyConfig"
    )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float moveStopDistance;
        [SerializeField] private float attackCoolDown;

        public float MoveStopDistance => moveStopDistance;
        public float AttackCoolDown => attackCoolDown;
    }
}