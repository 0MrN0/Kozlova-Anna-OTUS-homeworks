using R3;

namespace Lessons.Architecture.PM
{
    public sealed class Stat
    {
        public ReadOnlyReactiveProperty<int> Value => _value;
        public string Name { get; private set; }

        private readonly ReactiveProperty<int> _value;

        public Stat(string name, int value)
        {
            Name = name;
            _value = new(value);
        }

        public void ChangeValue(int value)
        {
            _value.Value = value;
        }
    }
}