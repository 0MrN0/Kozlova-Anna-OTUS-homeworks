using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent
    {
        public bool IsReached { get; private set; } = false;

        private readonly MoveComponent _moveComponent;
        private readonly LevelBounds _levelBounds;
        private readonly float _stopDistance = 0.25f;

        private Vector2 _destination;

        public EnemyMoveAgent(MoveComponent moveComponent, LevelBounds levelBounds, float stopDistance)
        {
            _moveComponent = moveComponent;
            _levelBounds = levelBounds;
            _stopDistance = stopDistance;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        public void Move()
        {
            if (IsReached)
            {
                return;
            }

            var vector = _destination - (Vector2)_moveComponent.transform.position;
            if (vector.magnitude <= _stopDistance)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.TryMoveByRigidbodyVelocity(direction, _levelBounds);
        }
    }
}