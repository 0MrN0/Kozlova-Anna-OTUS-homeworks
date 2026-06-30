using System;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class EnemyAttackAgentBase : MonoBehaviour
    {
        public Action<Vector2, Vector2> FireEvent;

        public abstract void SetTarget(Transform characterTransform);
    }
}