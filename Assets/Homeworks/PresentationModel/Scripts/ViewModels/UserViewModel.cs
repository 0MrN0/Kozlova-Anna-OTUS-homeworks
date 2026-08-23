using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserViewModel : IUserViewModel
    {
        public ReadOnlyReactiveProperty<Sprite> PortraitSprite => _portraitSprite;
        public ReadOnlyReactiveProperty<string> NameString => _nameString;
        public ReadOnlyReactiveProperty<string> DescriptionString => _descriptionString;

        private readonly ReactiveProperty<Sprite> _portraitSprite;
        private readonly ReactiveProperty<string> _nameString;
        private readonly ReactiveProperty<string> _descriptionString;

        private readonly CompositeDisposable _disposable = new();
        private readonly UserInfo _userInfo;
        private readonly NameStringFormat _nameStringFormat;
        private readonly DescriptionStringFormat _descriptionStringFormat;

        public UserViewModel(UserInfo userInfo, NameStringFormat nameStringFormat, DescriptionStringFormat descriptionStringFormat)
        {
            _userInfo = userInfo;
            _nameStringFormat = nameStringFormat;
            _descriptionStringFormat = descriptionStringFormat;

            _portraitSprite = new(_userInfo.Icon.CurrentValue);
            _nameString = new(ComputeNameString());
            _descriptionString = new(ComputeDescriptionString());

            _userInfo.Icon
                     .Subscribe(value => UpdatePortraitImage(value))
                     .AddTo(_disposable);
            _userInfo.Name
                     .Subscribe(_ => UpdateNameString())
                     .AddTo(_disposable);
            _userInfo.Description
                     .Subscribe(_ => UpdateDescriptionString())
                     .AddTo(_disposable);
        }

        private void UpdateDescriptionString()
        {
            _descriptionString.Value = ComputeDescriptionString();
        }

        private void UpdateNameString()
        {
            _nameString.Value = ComputeNameString();
        }

        private void UpdatePortraitImage(Sprite value)
        {
            _portraitSprite.Value = value;
        }

        private string ComputeDescriptionString()
        {
            return string.Format(_descriptionStringFormat.DescriptionFormat, _userInfo.Description.CurrentValue);
        }

        private string ComputeNameString()
        {
            return string.Format(_nameStringFormat.NameFormat, _userInfo.Name.CurrentValue);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}