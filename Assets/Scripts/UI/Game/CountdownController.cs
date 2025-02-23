using DG.Tweening;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Stores;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class CountdownController : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private TMP_Text _number;
        private int _currentCountdown = 3;
        private Sequence _sequence;

        private void Awake()
        {
            OneCycleRecordStore.IsCountdown.Value = true;

            _canvasGroup = GetComponent<CanvasGroup>();
            _number = GetComponentInChildren<TMP_Text>();
            _sequence = DOTween.Sequence();
        }

        private void Start()
        {
            for (var countdown = 3; countdown >= 0; countdown--)
            {
                _sequence.AppendCallback(Showing);
                _sequence.AppendInterval(1.0f);
                _sequence.AppendCallback(Hiding);
                _sequence.AppendInterval(0.3f);
            }

            _sequence.Play();
        }

        private void Showing()
        {
            AudioModule.Play(_currentCountdown <= 0 ? "start" : "countdown");

            _number.text = _currentCountdown <= 0 ? "START" : _currentCountdown.ToString();
            _number.DOFade(1.0f, 0.3f)
                .From(0.0f);
            _number.DOScale(1.0f, 0.3f)
                .From(Vector2.one * 1.3f);
        }

        private void Hiding()
        {
            _number.DOFade(0.0f, 0.3f)
                .From(1.0f);
            _number.DOScale(0.7f, 0.3f)
                .From(Vector2.one);

            if (_currentCountdown <= 0)
            {
                OneCycleRecordStore.IsCountdown.Value = false;

                _canvasGroup.DOFade(0.0f, 0.3f);
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;

                AudioModule.Play("in_game_bgm");
            }

            _currentCountdown--;
        }
    }
}