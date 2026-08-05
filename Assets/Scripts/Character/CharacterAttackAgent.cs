using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class CharacterAttackAgent : IStartable, IDisposable
    {
        private readonly BulletSystem _bulletSystem;
        private readonly InputManager _inputManager;
        private readonly PlayerBulletConfig _bulletConfig;
        private readonly CharacterComponentsHolder _componentsHolder;


        [Inject]
        public CharacterAttackAgent(BulletSystem bulletSystem,
                                    InputManager inputManager,
                                    PlayerBulletConfig bulletConfig,
                                    CharacterComponentsHolder componentsHolder)
        {
            _bulletSystem = bulletSystem;
            _inputManager = inputManager;
            _bulletConfig = bulletConfig;
            _componentsHolder = componentsHolder;
        }

        public void Start()
        {
            _inputManager.FireRequiredEvent += OnFlyBullet;
        }

        public void Dispose()
        {
            _inputManager.FireRequiredEvent -= OnFlyBullet;
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int)_bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = _componentsHolder.WeaponComponent.Position,
                velocity = _componentsHolder.WeaponComponent.Rotation * Vector3.up * _bulletConfig.speed
            });
        }
    }
}