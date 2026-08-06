using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        public Action<Vector2, Vector2> FireEvent { get; set; }

        private readonly WeaponComponent _weaponComponent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly float _countdown;

        private Transform _targetTransform;
        private float _currentTime;

        public EnemyAttackAgent(WeaponComponent weaponComponent,
                                EnemyMoveAgent moveAgent,
                                float countdown)
        {
            _weaponComponent = weaponComponent;
            _moveAgent = moveAgent;
            _countdown = countdown;
        }

        public void SetTarget(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        public void Attack()
        {
            if (!_moveAgent.IsReached)
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2)_targetTransform.position - startPosition;
            var direction = vector.normalized;
            FireEvent?.Invoke(startPosition, direction);
        }
    }
}