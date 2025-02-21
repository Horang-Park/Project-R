using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using UnityEngine.Rendering.Universal;

namespace UI.Main.Settings.Graphics
{
    public class UseHalfRenderScale : BaseGraphicController
    {
        private UniversalRenderPipelineAsset _injectedRenderPipeline;

        private const float OnRenderScale = 0.5f;
        private const float OffRenderScale = 1.0f;

        public void Initialize(UniversalRenderPipelineAsset renderPipelineAsset)
        {
            _injectedRenderPipeline = renderPipelineAsset;
        }

        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int("Use Half Render Scale", isOn ? 1 : 0);

            _injectedRenderPipeline.renderScale = isOn ? OnRenderScale : OffRenderScale;
        }
    }
}