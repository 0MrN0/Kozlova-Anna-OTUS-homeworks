using UnityEngine;

namespace Code.Gameplay.Player.View
{
    public sealed class PlayerFacade : MonoBehaviour
    {
        public PlayerMoverView MoverView => _moverView;
        public PlayerAnimatorView AnimatorView => _animatorView;

        [SerializeField] private PlayerMoverView _moverView;
        [SerializeField] private PlayerAnimatorView _animatorView;
    }
}