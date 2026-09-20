using UnityEngine;

namespace Code.Gameplay.Player
{
    [CreateAssetMenu(menuName = "Configs / Player's Config", fileName = "PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        public float MoveSpeed => _speed;
        public float MoveAnimationSpeed => _moveAnimationSpeed;
        public float IdleAnimationSpeed => _idleAnimationSpeed;

        [SerializeField] private float _speed;
        [SerializeField] private float _moveAnimationSpeed;
        [SerializeField] private float _idleAnimationSpeed;
    }
}