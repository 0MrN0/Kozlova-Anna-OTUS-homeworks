using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "Configs / Cube Config")]
    public sealed class CubeConfig : ScriptableObject
    {
        public Entity RedCubePrefab => _redCubePrefab;

        [SerializeField] private Entity _redCubePrefab;
    }
}