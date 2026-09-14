using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Infrastructure.SceneLoad
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeDuration = 0.4f;

        private void Awake()
        {
            if (transform.parent == null) DontDestroyOnLoad(this);
        }

        public void Show()
        {
            _curtain.DOKill();
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public async UniTask Hide()
        {
            await _curtain.DOFade(0f, _fadeDuration).ToUniTask();
            gameObject.SetActive(false);
        }
    }
}