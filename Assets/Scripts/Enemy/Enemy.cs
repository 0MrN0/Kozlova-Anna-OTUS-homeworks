using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class Enemy : MonoBehaviour
    {
        public HitPointsComponent HpComponent { get; private set; }
        public EnemyMoveAgent MoveAgent { get; private set; }
        public EnemyAttackAgent AttackAgent { get; private set; }

        public Action<Enemy> DeadEvent;

        private BulletSystem _bulletSystem;

        public void Init(BulletSystem bulletSystem)
        {
            HpComponent = GetComponent<HitPointsComponent>();
            HpComponent.Init();
            MoveAgent = GetComponent<EnemyMoveAgent>();
            AttackAgent = GetComponent<EnemyAttackAgent>();
            _bulletSystem = bulletSystem;
            Subscribe();
        }

        private void Subscribe()
        {
            HpComponent.HpEmptyEvent += OnDeath;
            AttackAgent.FireEvent += OnFire;
        }

        private void Unsubscribe()
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
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
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