using System;
using R3;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statText;

        private IDisposable _disposable;

        public void Init(IStatViewModel statViewModel)
        {
            _disposable?.Dispose();
            _disposable = statViewModel.StatString.Subscribe(value => UpdateStatText(value));
        }

        private void UpdateStatText(string value)
        {
            _statText.text = value;
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}