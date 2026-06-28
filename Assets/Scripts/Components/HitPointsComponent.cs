using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int hitPoints;

        public event Action HpEmptyEvent;

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                HpEmptyEvent?.Invoke();
            }
        }
    }
}