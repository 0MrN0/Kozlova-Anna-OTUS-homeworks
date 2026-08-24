using System;
using R3;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class StatView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _statText;

        private IDisposable _disposable;

        public void Init(IStatViewModel statViewModel)
        {
            _disposable?.Dispose();
            _disposable = statViewModel.StatString.Subscribe(value => UpdateStatText(value));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void UpdateStatText(string value)
        {
            _statText.text = value;
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}