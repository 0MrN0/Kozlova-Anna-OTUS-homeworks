using Code.Services.Factory;
using UnityEngine;

namespace Code.Views
{
    public sealed class AnimatorPoolReset : MonoBehaviour, IPoolable
    {
        [SerializeField] private Animator _animator;

        public void OnDespawned()
        {
            _animator.WriteDefaultValues();
        }

        public void OnSpawned()
        {
            _animator.Rebind();
            _animator.Update(0f);
        }
    }
}