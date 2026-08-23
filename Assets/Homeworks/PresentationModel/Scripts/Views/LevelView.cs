using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _experienceText;

        [Space]
        [SerializeField] private Image _barImage;
        [SerializeField] private Sprite _completedBarSprite;
        [SerializeField] private Sprite _uncompletedBarSprite;

        [Space]
        [SerializeField] private Button _levelUpButton;

        private ILevelViewModel _levelViewModel;
        private readonly CompositeDisposable _disposable = new();

        public void Init(ILevelViewModel levelViewModel)
        {
            _levelViewModel = levelViewModel;

            _levelUpButton.onClick.AddListener(LevelUp);

            _disposable.Clear();
            _levelViewModel.CanLevelUp
                           .Subscribe(value => OnCanLevelUpChanged(value))
                           .AddTo(_disposable);
            _levelViewModel.LevelString
                           .Subscribe(value => UpdateLevelText(value))
                           .AddTo(_disposable);
            _levelViewModel.ExperienceString
                           .Subscribe(value => UpdateExperienceText(value))
                           .AddTo(_disposable);
            _levelViewModel.ExperienceProgress
                           .Subscribe(value => UpdateBarImageFill(value))
                           .AddTo(_disposable);
        }

        private void UpdateBarImageFill(float value)
        {
            _barImage.fillAmount = value;
        }

        private void OnCanLevelUpChanged(bool value)
        {
            _levelUpButton.interactable = value;
            _barImage.sprite = value ? _completedBarSprite : _uncompletedBarSprite;
        }

        private void UpdateLevelText(string value)
        {
            _levelText.text = value;
        }

        private void UpdateExperienceText(string value)
        {
            _experienceText.text = value;
        }

        private void LevelUp()
        {
            if (!_levelViewModel.CanLevelUp.CurrentValue) return;

            _levelViewModel.LevelUp();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}