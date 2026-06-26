using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached
        {
            get { return isReached; }
        }

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 destination;

        private bool isReached;

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            isReached = false;
        }

        private void FixedUpdate()
        {
            if (isReached)
            {
                return;
            }
            
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}