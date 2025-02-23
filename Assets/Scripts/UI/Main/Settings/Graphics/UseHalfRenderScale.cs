using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;
using UnityEngine.Rendering.Universal;

namespace UI.Main.Settings.Graphics
{
    public class UseHalfRenderScale : BaseGraphicController
    {
        private UniversalRenderPipelineAsset _injectedRenderPipeline;

        private const float OnRenderScale = 0.7f;
        private const float OffRenderScale = 1.0f;

        public void Initialize(UniversalRenderPipelineAsset renderPipelineAsset)
        {
            _injectedRenderPipeline = renderPipelineAsset;
        }

        public override void OnShowSettings()
        {
            Toggle.isOn = SettingsStore.IsHalfRenderScaleUse;
        }

        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int(ConstantStore.UseHalfRenderScaleSaveKey, isOn ? 1 : 0);

            _injectedRenderPipeline.renderScale = isOn ? OnRenderScale : OffRenderScale;
        }
    }
}