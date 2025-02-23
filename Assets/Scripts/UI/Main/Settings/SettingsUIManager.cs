using DG.Tweening;
using UI.Main.Credit;
using UI.Main.Settings.Graphics;
using UI.Main.Settings.Sounds;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI.Main.Settings
{
    public class SettingsUIManager : MonoBehaviour
    {
        [Header("Top Menu")]
        [SerializeField] private Button exitButton;
        [Header("Contents")]
        [SerializeField] private Button creditButton;
        [Header("Resources")]
        [SerializeField] private UniversalRenderPipelineAsset universalRenderPipelineAsset;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        // Sounds
        private BgmController _bgmController;
        private SfxController _sfxController;

        // Graphics
        private UseHalfRenderScale _useHalfRenderScale;
        private UseAntiAliasing _useAntiAliasing;
        private UsePostProcessing _usePostProcessing;

        private CreditUIManager _creditUIManager;

        public void Show()
        {
            _bgmController.OnShowSettings();
            _sfxController.OnShowSettings();

            _useHalfRenderScale.OnShowSettings();
            _useAntiAliasing.OnShowSettings();
            _usePostProcessing.OnShowSettings();

            _canvasGroup.DOFade(1.0f, 0.3f)
                .From(0.0f)
                .OnPlay(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                })
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                });
            _rectTransform.DOAnchorPosY(0.0f, 0.2f)
                .From(new Vector2(0.0f, -40.0f));
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();

            _bgmController = GetComponentInChildren<BgmController>();
            _sfxController = GetComponentInChildren<SfxController>();

            _useHalfRenderScale = GetComponentInChildren<UseHalfRenderScale>();
            _useAntiAliasing = GetComponentInChildren<UseAntiAliasing>();
            _usePostProcessing = GetComponentInChildren<UsePostProcessing>();

            _creditUIManager = GetComponentInChildren<CreditUIManager>();
        }

        private void Start()
        {
            exitButton.onClick.AddListener(Exit);

            _useHalfRenderScale.Initialize(universalRenderPipelineAsset);
            _useAntiAliasing.Initialize(universalRenderPipelineAsset);

            creditButton.onClick.AddListener(_creditUIManager.Show);
        }

        private void Exit()
        {
            PlayerPrefs.Save();

            _canvasGroup.DOFade(0.0f, 0.2f)
                .From(1.0f)
                .OnPlay(() =>
                {
                    _canvasGroup.interactable = false;
                })
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                });

            _rectTransform.DOAnchorPosY(-40.0f, 0.3f)
                .From(Vector2.zero);
        }
    }
}