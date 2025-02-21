using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Main.Settings
{
    public class SettingsUIManager : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        public void Show()
        {
            _canvasGroup.DOFade(1.0f, 0.5f)
                .From(0.0f)
                .OnPlay(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                })
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                });
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Exit()
        {
            _canvasGroup.DOFade(0.0f, 0.5f)
                .From(1.0f)
                .OnPlay(() =>
                {
                    _canvasGroup.interactable = false;
                })
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                });
        }
    }
}