using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class SceneStartController: MonoBehaviour
    {
        [SerializeField] private SceneCycleRunner sceneCycleRunner;
        [SerializeField] private Button startGameButton;
        [SerializeField] private TMP_Text countDownText;
        [SerializeField] private int countDown = 3;
        
        private WaitForSeconds _waitForSeconds1 = new(1f);

        private void Awake()
        {
            countDownText.text = "";
            startGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            StartCoroutine(StartRoutine());
        }

        private IEnumerator StartRoutine()
        {
            for (var i = countDown; i > 0; i--)
            {
                countDownText.text = i.ToString();
                yield return _waitForSeconds1;
            }

            startGameButton.onClick.RemoveAllListeners();
            countDownText.gameObject.SetActive(false);
            startGameButton.gameObject.SetActive(false);
            sceneCycleRunner.gameObject.SetActive(true);
        }
    }
}