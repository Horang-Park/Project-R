using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI.Main.Settings.Graphics
{
    public class UseHalfRenderScale : MonoBehaviour
    {
        private Toggle _toggle;
        private UniversalRenderPipelineAsset _injectedRenderPipeline;

        private const float OnRenderScale = 0.5f;
        private const float OffRenderScale = 1.0f;

        public void Initialize(UniversalRenderPipelineAsset renderPipelineAsset)
        {
            _injectedRenderPipeline = renderPipelineAsset;

            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(RenderScale);
        }

        private void RenderScale(bool isOn)
        {
            SetPlayerPrefs.Int("Use Half Render Scale", isOn ? 1 : 0);

            _injectedRenderPipeline.renderScale = isOn ? OnRenderScale : OffRenderScale;
        }
    }
}