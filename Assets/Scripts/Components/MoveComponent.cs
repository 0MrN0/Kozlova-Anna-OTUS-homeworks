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
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rb.position + vector * speed;
            _rb.MovePosition(nextPosition);
        }
    }
}