using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class CharacterAttackAgent : MonoBehaviour
    {        
        [SerializeField] private InputManager inputManager;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private CharacterComponentsHolder componentsHolder;

        private BulletSystem _bulletSystem;

        [Preserve]
        [Inject]
        private void Construct(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        private void OnEnable()
        {
            inputManager.FireRequiredEvent += OnFlyBullet;
        }

        private void OnDisable()
        {
            inputManager.FireRequiredEvent -= OnFlyBullet;
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = componentsHolder.WeaponComponent.Position,
                velocity = componentsHolder.WeaponComponent.Rotation * Vector3.up * bulletConfig.speed
            });
        }
    }
}