using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo
    {
        public ReadOnlyReactiveProperty<string> Name => _name;
        public ReadOnlyReactiveProperty<string> Description => _description;
        public ReadOnlyReactiveProperty<Sprite> Icon => _icon;

        private readonly ReactiveProperty<string> _name;
        private readonly ReactiveProperty<string> _description;
        private readonly ReactiveProperty<Sprite> _icon;

        public UserInfo(string name, string description, Sprite icon)
        {
            _name = new(name);
            _description = new(description);
            _icon = new(icon);
        }

        public void ChangeName(string name)
        {
            _name.Value = name;
        }

        public void ChangeDescription(string description)
        {
            _description.Value = description;
        }

        public void ChangeIcon(Sprite icon)
        {
            _icon.Value = icon;
        }
    }
}