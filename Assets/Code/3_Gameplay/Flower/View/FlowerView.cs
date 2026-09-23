using System;
using UnityEngine;

namespace Code.Gameplay.Flower.View
{
    public sealed class FlowerView : MonoBehaviour, IFlowerView
    {
        public Vector2 Position => transform.position;

        public event Action Pickupped;

        private void OnTriggerEnter(Collider other)
        {
            Pickupped?.Invoke();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}