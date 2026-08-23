using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class UserHelper : MonoBehaviour
    {
        [SerializeField] private UserView _userView;

        private UserViewModelFactory _factory;
        private IUserViewModel _currentViewModel;
        private UserInfo _currentUserInfo;

        [Inject]
        public void Construct(UserViewModelFactory factory)
        {
            _factory = factory;
        }

        public void Show(UserInfo userInfo)
        {
            _currentViewModel?.Dispose();
            _currentUserInfo = userInfo;
            _currentViewModel = _factory.Create(userInfo);
            _userView.Init(_currentViewModel);
        }

        public void ChangeName(string name)
        {
            _currentUserInfo?.ChangeName(name);
        }

        public void ChangeDescription(string description)
        {
            _currentUserInfo?.ChangeDescription(description);
        }
    }
}
