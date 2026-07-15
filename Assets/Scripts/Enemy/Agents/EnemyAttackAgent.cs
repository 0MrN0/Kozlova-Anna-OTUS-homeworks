using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : EnemyAttackAgentBase, ISceneCycleFixedUpdate
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgentBase moveAgent;
        [SerializeField] private float countdown;

        private Transform _targetTransform;
        private float _currentTime;

        public override void SetTarget(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        public void OnFixedUpdate()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += countdown;
            }
        }

        private void Fire()
        {
            var startPosition = weaponComponent.Position;
            var vector = (Vector2)_targetTransform.position - startPosition;
            var direction = vector.normalized;
            FireEvent?.Invoke(startPosition, direction);
        }
    }
}