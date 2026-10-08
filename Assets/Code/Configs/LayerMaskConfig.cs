using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "LayerMaskConfig", menuName = "Configs / Layer Mask Config")]
    public sealed class LayerMaskConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask GroundMask { get; private set; }
        [field: SerializeField] public LayerMask BodyMask { get; private set; }
    }
}