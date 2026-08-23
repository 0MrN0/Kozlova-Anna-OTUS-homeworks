using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class UserView : MonoBehaviour
    {
        [SerializeField] private Image _portrait;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;

        private IUserViewModel _userViewModel;
        private readonly CompositeDisposable _disposable = new();

        public void Init(IUserViewModel userViewModel)
        {
            _userViewModel = userViewModel;

            _disposable.Clear();
            _userViewModel.PortraitSprite
                          .Subscribe(value => OnPortraitChanged(value))
                          .AddTo(_disposable);
            _userViewModel.NameString
                          .Subscribe(value => OnNameChanged(value))
                          .AddTo(_disposable);
            _userViewModel.DescriptionString
                          .Subscribe(value => OnDescriptionChanged(value))
                          .AddTo(_disposable);
        }

        private void OnPortraitChanged(Sprite value)
        {
            _portrait.sprite = value;
        }

        private void OnNameChanged(string value)
        {
            _nameText.text = value;
        }

        private void OnDescriptionChanged(string value)
        {
            _descriptionText.text = value;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}