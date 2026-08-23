using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data / New PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int CurrentLevel => _currentLevel;
        public int CurrentExperience => _currentExperience;
        public IReadOnlyList<StatData> Stats => _stats;

        [Header("Base Info")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;

        [Header("Level Info")]
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _currentExperience;

        [Header("Stats Info")]
        [SerializeField] private List<StatData> _stats = new();
    }
}