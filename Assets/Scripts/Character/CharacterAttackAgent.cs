using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterAttackAgent : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private CharacterComponentsHolder componentsHolder;

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
            bulletSystem.FlyBulletByArgs(new BulletSystem.Args
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