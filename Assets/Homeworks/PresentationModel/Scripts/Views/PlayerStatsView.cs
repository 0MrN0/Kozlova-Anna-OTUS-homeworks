using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private StatView[] _statViews;

        public void Init(IPlayerStatsViewModel viewModel)
        {
            for (var i = 0; i < _statViews.Length; i++)
            {
                _statViews[i].Init(viewModel.Stats[i]);
            }
        }
    }
}
