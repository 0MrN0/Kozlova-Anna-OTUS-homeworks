using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public class CharacterAttackAgent : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;

        private WeaponComponent _weapon;

        private void Awake()
        {
            _weapon = GetComponent<WeaponComponent>();
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
            bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = _weapon.Position,
                velocity = _weapon.Rotation * Vector3.up * bulletConfig.speed
            });
        }
    }
}