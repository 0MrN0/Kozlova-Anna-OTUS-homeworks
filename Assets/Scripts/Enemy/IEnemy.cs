using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemy
    {
        public Action<IEnemy> DeadEvent { get; set; }
        public EnemyComponentsHolder ComponentsHolder { get; }

        public void Subscribe();
        public void Unsubscribe();
        public void Move();
        public void Attack();
        public void SetDestination(Vector3 destination);
        public void SetTarget(Transform target);
    }
}