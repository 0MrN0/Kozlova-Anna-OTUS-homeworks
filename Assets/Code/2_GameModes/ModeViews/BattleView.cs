using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.GameModes.ModeViews
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private Button _exitToMenuButton;

        private IExitToMenuRequester _exitToMenuRequester;

        [Inject]
        public void Construct(IExitToMenuRequester exitToMenuRequester)
        {
            _exitToMenuRequester = exitToMenuRequester;
        }

        private void Start()
        {
            _exitToMenuButton.onClick.AddListener(_exitToMenuRequester.RequestExitToMenu);
        }

        private void OnDestroy()
        {
            _exitToMenuButton.onClick.RemoveListener(_exitToMenuRequester.RequestExitToMenu);
        }
    }
}