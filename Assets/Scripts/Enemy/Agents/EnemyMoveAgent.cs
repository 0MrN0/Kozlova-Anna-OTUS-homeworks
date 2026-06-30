using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : EnemyMoveAgentBase
    {
        [SerializeField] private MoveComponent moveComponent;

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
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}