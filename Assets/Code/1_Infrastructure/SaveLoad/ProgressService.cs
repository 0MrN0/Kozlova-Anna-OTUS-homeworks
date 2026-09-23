using Code.Core.Data;
using UnityEngine;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class ProgressService : IProgressService
    {
        public PlayerProgress Progress { get; private set; } = new PlayerProgress();

        public PlayerProgress CreateNewProgress() => Progress = new PlayerProgress();

        public string ToJson() => JsonUtility.ToJson(Progress);
    }
}
