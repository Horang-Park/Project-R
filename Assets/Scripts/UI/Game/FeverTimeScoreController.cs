using System.Globalization;
using DG.Tweening;
using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Game
{
    public class FeverTimeScoreController : MonoBehaviour
    {
        private TMP_Text _text;
        private TMP_Text _plusText;
        private int _currentValue;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _plusText = transform.parent.GetChild(0).GetComponent<TMP_Text>();
        }

        private void Start()
        {
            OneCycleRecordStore.FeverTimeScore
                .Subscribe(ScoreUpdate)
                .AddTo(this);

            OneCycleRecordStore.IsFeverTime
                .Subscribe(OnOff)
                .AddTo(this);
        }

        private void ScoreUpdate(int score)
        {
            _text.DOKill();
            _text.DOCounter(_currentValue, score, 0.3f, true, CultureInfo.CurrentCulture)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => _currentValue = score)
                .OnKill(() =>
                {
                    _text.text = score.ToString("#0");
                    _currentValue = score;
                });
        }

        private void OnOff(bool isFeverTime)
        {
            _text.DOFade(isFeverTime ? 1.0f : 0.0f, 0.3f)
                .OnKill(() =>
                {
                    _text.alpha = isFeverTime ? 1.0f : 0.0f;
                });
            _plusText.DOFade(isFeverTime ? 1.0f : 0.0f, 0.3f)
                .OnKill(() =>
                {
                    _text.alpha = isFeverTime ? 1.0f : 0.0f;
                });
        }
    }
}