using DG.Tweening;
using UnityEngine;

namespace UI.Game.GameOver
{
    public class HighScoreController : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _logoParent;
        private CanvasGroup _logoParentCanvasGroup;

        public void Show()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_canvasGroup.DOFade(1.0f, 0.1f));
            sequence.AppendCallback(() =>
            {
                _logoParentCanvasGroup.DOFade(1.0f, 0.2f);
                _logoParent.DOAnchorPosY(0.0f, 0.3f).From(new Vector2(0.0f, -100.0f));
            });

            sequence.AppendInterval(3.0f);

            sequence.AppendCallback(() =>
            {
                _logoParent.DOAnchorPosY(150.0f, 0.2f).From(new Vector2(0.0f, 0.0f));
                _logoParentCanvasGroup.DOFade(0.0f, 0.3f);
            });
            sequence.Append(_canvasGroup.DOFade(0.0f, 0.5f));

            sequence.Play();
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _logoParent = transform.GetChild(0).GetComponent<RectTransform>();
            _logoParentCanvasGroup = _logoParent.GetComponent<CanvasGroup>();
        }
    }
}