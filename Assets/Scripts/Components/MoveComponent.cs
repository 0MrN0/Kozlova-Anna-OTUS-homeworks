using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void TryMoveByRigidbodyVelocity(Vector2 vector, LevelBounds levelBounds)
        {
            var nextPosition = _rb.position + vector * speed;
            if (!levelBounds.InBounds(nextPosition)) return;
            
            _rb.MovePosition(nextPosition);
        }
    }
}