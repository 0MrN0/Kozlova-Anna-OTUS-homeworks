using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "StatStringFormat", menuName = "StringFormats / new StatStringFormat")]
    public sealed class StatStringFormat : ScriptableObject
    {
        public string StatFormat => _statFormat;

        [SerializeField] private string _statFormat = "{0}: {1}";
    }
}