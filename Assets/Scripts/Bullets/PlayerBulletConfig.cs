using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New Player BulletConfig"
    )]
    public sealed class PlayerBulletConfig : ScriptableObject
    {
        public PhysicsLayer physicsLayer;
        public Color color;
        public int damage;
        public float speed;
    }
}