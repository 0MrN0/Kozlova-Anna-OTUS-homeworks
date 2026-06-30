using UnityEngine;

namespace ShootEmUp
{
    public abstract class EnemyMoveAgentBase : MonoBehaviour
    {
        public bool IsReached { get; protected set; }

        public abstract void SetDestination(Vector2 position);
    }
}