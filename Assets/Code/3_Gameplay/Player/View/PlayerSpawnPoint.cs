using UnityEngine;

namespace Code.Gameplay.Player.View
{
    public sealed class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
    }
}