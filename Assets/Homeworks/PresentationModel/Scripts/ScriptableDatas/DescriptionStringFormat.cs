using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "DescriptionStringFormat", menuName = "StringFormats / new DescriptionStringFormat")]
    public class DescriptionStringFormat : ScriptableObject
    {
        public string DescriptionFormat => _descriptionFormat;

        [SerializeField] private string _descriptionFormat = "{0}";
    }
}