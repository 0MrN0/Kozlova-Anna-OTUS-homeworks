using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        private void OnEnable()
        {
            character.GetComponent<HitPointsComponent>().hpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            character.GetComponent<HitPointsComponent>().hpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();

        private void FixedUpdate()
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) _bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.speed
            });
        }
    }
}