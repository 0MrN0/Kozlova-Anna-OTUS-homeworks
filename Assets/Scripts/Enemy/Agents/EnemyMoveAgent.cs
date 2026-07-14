using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : EnemyMoveAgentBase
    {
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private float stopDistance = 0.25f;

        private Vector2 _destination;

        public override void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        private void FixedUpdate()
        {
            if (IsReached)
            {
                return;
            }
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= stopDistance)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}