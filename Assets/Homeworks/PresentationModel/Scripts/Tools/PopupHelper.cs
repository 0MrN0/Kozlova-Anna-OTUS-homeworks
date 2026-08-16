using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PopupHelper : MonoBehaviour
    {
        [SerializeField] private IPopupView _playerPopup;

        public void ShowPlayerPopup()
        {
            _playerPopup.Show(new PlayerPopupViewModel());
        }
    }
}