using UnityEngine;

namespace Code.Gameplay.LevelField
{
    public sealed class GameField : MonoBehaviour
    {
        public float Width => _width;
        public float Height => _height;

        [SerializeField] private float _width;
        [SerializeField] private float _height;

        public bool InBounds(Vector2 pos)
        {
            Vector2 center = transform.position;
            Vector2 halfSize = new Vector2(_width, _height) * 0.5f;

            return Mathf.Abs(pos.x - center.x) <= halfSize.x && Mathf.Abs(pos.y - center.y) <= halfSize.y;
        }
    }
}
