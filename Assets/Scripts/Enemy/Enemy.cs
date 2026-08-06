using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class Enemy : IEnemy
    {
        public Action<IEnemy> DeadEvent { get; set; }
        public EnemyComponentsHolder ComponentsHolder { get; private set; }
        
        private EnemyDeathAgent _deathAgent;
        private EnemyMoveAgent _moveAgent;
        private EnemyAttackAgent _attackAgent;

        private readonly BulletSystem _bulletSystem;
        private readonly EnemyConfig _enemyConfig;

        [Inject]
        public Enemy(BulletSystem bulletSystem,
                     EnemyConfig enemyConfig)
        {
            _bulletSystem = bulletSystem;
            _enemyConfig = enemyConfig;
        }

        public void Init(EnemyComponentsHolder componentsHolder)
        {
            ComponentsHolder = componentsHolder;
            _deathAgent = new(ComponentsHolder.HpComponent);
            _moveAgent = new(ComponentsHolder.MoveComponent, _enemyConfig.MoveStopDistance);
            _attackAgent = new(ComponentsHolder.WeaponComponent, _moveAgent, _enemyConfig.AttackCoolDown);
        }

        public void Subscribe()
        {
            _deathAgent.HpComponent.HpEmptyEvent += OnDeath;
            _attackAgent.FireEvent += OnFire;
        }

        public void Unsubscribe()
        {
            _deathAgent.HpComponent.HpEmptyEvent -= OnDeath;
            _attackAgent.FireEvent -= OnFire;
        }

        public void Move()
        {
            _moveAgent.Move();
        }

        public void Attack()
        {
            _attackAgent.Attack();
        }

        public void SetDestination(Vector3 destination)
        {
            _moveAgent.SetDestination(destination);
        }

        public void SetTarget(Transform target)
        {
            _attackAgent.SetTarget(target);
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