using DG.Tweening;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Credit
{
    public class CreditUIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform scroll;

        private CanvasGroup _canvasGroup;
        private Button _exit;

        public void Show()
        {
            _canvasGroup.DOFade(1.0f, 0.5f)
                .OnPlay(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                })
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;

                    AudioModule.Pause("main_bgm");
                    AudioModule.Play("credit_bgm");

                    AutoScroll();
                });
        }


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _exit = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _exit.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            _canvasGroup.DOFade(0.0f, 0.5f)
                .OnPlay(() =>
                {
                    _canvasGroup.interactable = false;

                    scroll.DOKill();
                })
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;

                    AudioModule.Stop("credit_bgm");
                    AudioModule.Resume("main_bgm");

                    scroll.anchoredPosition = Vector2.zero;
                });
        }

        private void AutoScroll()
        {
            scroll.DOAnchorPosY(1814.935f, 60.0f)
                .From(Vector2.zero)
                .SetEase(Ease.Linear);
        }
    }
}
