using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsView : MonoBehaviour, IDisposable
    {
        [SerializeField] private StatView[] _statViews;

        public void Init(IPlayerStatsViewModel viewModel)
        {
            Debug.Assert(_statViews.Length == viewModel.Stats.Count, "StatView slot count doesn't match PlayerStats count");

            for (var i = 0; i < _statViews.Length; i++)
            {
                _statViews[i].Init(viewModel.Stats[i]);
            }
        }

        public void Dispose()
        {
            foreach (var statView in _statViews)
            {
                statView.Dispose();
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
