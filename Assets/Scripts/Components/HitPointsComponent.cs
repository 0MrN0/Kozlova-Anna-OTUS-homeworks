using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int hitPoints;

        public event Action HpEmptyEvent;

        private int _curHp;

        public void Init()
        {
            _curHp = hitPoints;
        }

        public void TakeDamage(int damage)
        {
            _curHp -= damage;
            if (_curHp <= 0)
            {
                HpEmptyEvent?.Invoke();
            }
        }
    }
}