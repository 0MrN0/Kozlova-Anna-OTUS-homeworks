using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Gameplay.ScoreSystem
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _textChangeRate;

        private Sequence _sequence;
        private long _score;

        public void SetupScore(long score)
        {
            SetTextScore(score);
        }

        public void SetupScoreWithAnimation(long score)
        {
            AnimateText(_score, score);
        }

        private void AnimateText(long currentValue, long newValue)
        {
            _sequence?.Kill();
            _sequence = DOTween
                        .Sequence()
                        .Append(DOTween.To(() => currentValue,
                                            SetTextScore,
                                            newValue,
                                            _textChangeRate));
        }

        private void SetTextScore(long value)
        {
            _score = value;
            _scoreText.text = value.ToString();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
            _sequence = null;
        }
    }
}