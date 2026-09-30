using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "FieldConfig", menuName = "Configs / Field Config")]
    public sealed class FieldConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 MinPosition { get; private set; } = new(-50, 1, -50);
        [field: SerializeField] public Vector3 MaxPosition { get; private set; } = new(50, 25, 50);
    }
}