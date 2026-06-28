using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);

        public event FireHandler FireEvent;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private Transform _targetTranform;
        private float _currentTime;

        public void SetTarget(Transform targetTransform)
        {
            _targetTranform = targetTransform;
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        private void FixedUpdate()
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
            var vector = (Vector2)_targetTranform.transform.position - startPosition;
            var direction = vector.normalized;
            FireEvent?.Invoke(startPosition, direction);
        }
    }
}