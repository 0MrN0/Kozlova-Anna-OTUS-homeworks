using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IUserViewModel : IViewModel
    {
        public ReadOnlyReactiveProperty<Sprite> PortraitSprite { get; }
        public ReadOnlyReactiveProperty<string> NameString { get; }
        public ReadOnlyReactiveProperty<string> DescriptionString { get; }
    }
}