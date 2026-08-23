using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "LevelStringFormat", menuName = "StringFormats / new LevelStringFormat")]
    public sealed class LevelStringFormat : ScriptableObject
    {
        public string LevelFormat => _levelFormat;
        public string ExperienceFormat => _experienceFormat;

        [SerializeField] private string _levelFormat = "Level: {0}";
        [SerializeField] private string _experienceFormat = "XP: {0}/{1}";
    }
}