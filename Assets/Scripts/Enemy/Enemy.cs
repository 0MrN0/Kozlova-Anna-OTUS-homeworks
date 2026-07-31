using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgentBase))]
    [RequireComponent(typeof(EnemyAttackAgentBase))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class Enemy : EnemyBase
    {
        public HitPointsComponent HpComponent { get; private set; }

        private BulletSystem _bulletSystem;

        [Preserve]
        [Inject]
        private void Construct(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public override void Init()
        {
            HpComponent = GetComponent<HitPointsComponent>();
            HpComponent.Init();
            MoveAgent = GetComponent<EnemyMoveAgentBase>();
            AttackAgent = GetComponent<EnemyAttackAgentBase>();
        }

        public override void Subscribe()
        {
            HpComponent.HpEmptyEvent += OnDeath;
            AttackAgent.FireEvent += OnFire;
        }

        public override void Unsubscribe()
        {
            HpComponent.HpEmptyEvent -= OnDeath;
            AttackAgent.FireEvent -= OnFire;
        }

        private void OnDeath()
        {
            Unsubscribe();
            DeadEvent?.Invoke(this);
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = false,
                physicsLayer = (int)PhysicsLayer.ENEMY_BULLET,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }
    }
}