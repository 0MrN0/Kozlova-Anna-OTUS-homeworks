using DG.Tweening;
using UnityEngine;

namespace Code.Core.UI
{
    public sealed class ImageRotator : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DORotate(new Vector3(0, 0, -360),
                                1f,
                                RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}