using System.Collections.Generic;
using Code.Core.Contracts;
using UnityEngine;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class SaveLoadAggregate : ISaveLoadAggregate
    {
        private const string ProgressKey = "ProgressKey";

        public bool IsNeedLoadGame { get; private set; }

        private readonly IProgressService _progress;
        private readonly ISaveStorage _storage;

        private readonly List<ISaveLoad> _sceneParticipants = new();

        public SaveLoadAggregate(IProgressService progress, ISaveStorage storage)
        {
            _progress = progress;
            _storage = storage;
        }

        public void Save()
        {
            _progress.Progress.Flowers.Clear();
            SaveDistribute();

            string json = _progress.ToJson();
            _storage.Write(ProgressKey, json);
        }

        public void Register(ISaveLoad participant)
        {
            if (participant != null && !_sceneParticipants.Contains(participant))
                _sceneParticipants.Add(participant);
        }

        public void Unregister(ISaveLoad participant)
        {
            if (participant != null && _sceneParticipants.Contains(participant))
                _sceneParticipants.Remove(participant);
        }

        public void ReadProgress()
        {
            string json = _storage.Read(ProgressKey);

            if (string.IsNullOrEmpty(json))
                _progress.CreateNewProgress();
            else
                JsonUtility.FromJsonOverwrite(json, _progress.Progress);
        }

        public void ApplyProgress()
        {
            if (IsNeedLoadGame)
                LoadDistribute();
        }

        public void SetNeedLoadGame(bool isNeed) => IsNeedLoadGame = isNeed;

        public void Cleanup() => _sceneParticipants.Clear();

        private void LoadDistribute()
        {
            foreach (ISaveLoad saveLoad in _sceneParticipants)
                saveLoad.Load(_progress.Progress);
        }

        private void SaveDistribute()
        {
            foreach (ISaveLoad saveLoad in _sceneParticipants)
                saveLoad.Save(_progress.Progress);
        }
    }
}
