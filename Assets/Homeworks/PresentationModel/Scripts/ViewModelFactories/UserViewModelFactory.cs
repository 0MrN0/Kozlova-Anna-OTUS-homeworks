using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class UserViewModelFactory
    {
        private readonly NameStringFormat _nameStringFormat;
        private readonly DescriptionStringFormat _descriptionStringFormat;

        [Inject]
        public UserViewModelFactory(NameStringFormat nameStringFormat, DescriptionStringFormat descriptionStringFormat)
        {
            _nameStringFormat = nameStringFormat;
            _descriptionStringFormat = descriptionStringFormat;
        }

        public IUserViewModel Create(UserInfo userInfo)
        {
            return new UserViewModel(userInfo, _nameStringFormat, _descriptionStringFormat);
        }
    }
}
