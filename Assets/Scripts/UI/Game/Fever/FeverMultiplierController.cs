using DG.Tweening;
using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Game.Fever
{
    public class FeverMultiplierController : MonoBehaviour
    {
        private TMP_Text _text;
        private Tweener _shakeTweener;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            OneCycleRecordStore.CurrentFeverMultiplier
                .Subscribe(MultiplierUpdater)
                .AddTo(this);
        }

        private void MultiplierUpdater(int multiplier)
        {
            _text.DOScale(1.0f, 0.5f)
                .From(Vector3.one * 1.2f);
            _text.text = $"x{multiplier * 0.1f:F1}";

            switch (multiplier)
            {
                case 20:
                    Shaking();
                    break;
                case 10:
                    StopShaking();
                    break;
            }
        }

        private void Shaking()
        {
            _shakeTweener =  _text.rectTransform.DOShakePosition(1.0f, 5.0f, 15, fadeOut: false)
                .SetLoops(-1);
        }

        private void StopShaking()
        {
            _shakeTweener.Kill();
            
            _text.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
