using Zenject;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class ProgressApplier : IInitializable
    {
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public ProgressApplier(ISaveLoadAggregate saveLoadAggregate)
        {
            _saveLoadAggregate = saveLoadAggregate;
        }

        public void Initialize()
        {
            _saveLoadAggregate.ApplyProgress();
        }
    }
}