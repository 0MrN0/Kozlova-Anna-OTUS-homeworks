using UnityEngine;

namespace Code.Gameplay.Player.View
{
    public interface IPlayerMoverView
    {
        Vector2 GetPosition();
        public void Move(Vector3 direction);
        void SetPosition(Vector2 playerPosition);
    }
}