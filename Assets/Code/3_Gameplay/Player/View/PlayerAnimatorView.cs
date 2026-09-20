using UnityEngine;

namespace Code.Gameplay.Player.View
{
    public sealed class PlayerAnimatorView : MonoBehaviour, IPlayerAnimatorView
    {
        private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");
        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        
        [SerializeField] private Animator _animator;

        public void SetMoveX(float x)
        {
            _animator.SetFloat(MoveXHash, x);
        }

        public void SetMoveY(float y)
        {
            _animator.SetFloat(MoveYHash, y);
        }

        public void SetAnimationSpeed(float s)
        {
            _animator.SetFloat(MoveSpeedHash, s);
        }
    }
}