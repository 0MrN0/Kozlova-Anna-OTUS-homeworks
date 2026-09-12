using System;
using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.SceneLoad
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public CanvasGroup Curtain;
        public static Action OnHide;

        private void Awake()
        {
            if (transform.parent == null) DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
            Debug.Log("Show");
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.04f;
                yield return new WaitForSeconds(0.02f);
            }

            OnHide?.Invoke();
            gameObject.SetActive(false);
        }
    }
}