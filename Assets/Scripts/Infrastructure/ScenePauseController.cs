using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class ScenePauseController : MonoBehaviour, ISceneCycleAwake, ISceneCycleOnDestroy
    {
        [SerializeField] private SceneCycleRunner sceneCycleRunner;
        [SerializeField] private Button pauseResumeButton;
        [SerializeField] private TMP_Text pauseResumeButtonText;

        public void OnAwake()
        {
            pauseResumeButton.interactable = true;
            SwitchButtonToPause();
        }

        public void OnOnDestroy()
        {
            pauseResumeButton.onClick.RemoveAllListeners();
        }

        private void PauseRunner()
        {
            sceneCycleRunner.enabled = false;
            SwitchButtonToResume();
        }

        private void ResumeRunner()
        {
            sceneCycleRunner.enabled = true;
            SwitchButtonToPause();
        }

        private void SwitchButtonToPause()
        {
            pauseResumeButton.onClick.RemoveAllListeners();
            pauseResumeButton.onClick.AddListener(PauseRunner);
            pauseResumeButtonText.text = "Pause Game";
        }

        private void SwitchButtonToResume()
        {
            pauseResumeButton.onClick.RemoveAllListeners();
            pauseResumeButton.onClick.AddListener(ResumeRunner);
            pauseResumeButtonText.text = "Resume Game";
        }
    }
}