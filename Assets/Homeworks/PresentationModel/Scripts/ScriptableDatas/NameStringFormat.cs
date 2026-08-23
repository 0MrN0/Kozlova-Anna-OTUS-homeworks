using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "NameStringFormat", menuName = "StringFormats / new NameStringFormat")]
    public class NameStringFormat : ScriptableObject
    {
        public string NameFormat => _nameFormat;

        [SerializeField] private string _nameFormat = "@{0}";
    }
}