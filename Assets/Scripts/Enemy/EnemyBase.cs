using System;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public Action<EnemyBase> DeadEvent;
        public EnemyMoveAgentBase MoveAgent;
        public EnemyAttackAgentBase AttackAgent;

        public abstract void Init();
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}