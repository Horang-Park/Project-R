using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;
using UnityEngine.Rendering.Universal;

namespace UI.Main.Settings.Graphics
{
    public class UseAntiAliasing : BaseGraphicController
    {
        private UniversalRenderPipelineAsset _injectedRenderPipeline;

        private const int OnMsaa = 4; // 4x
        private const int OffMsaa = 1; // Disabled

        public void Initialize(UniversalRenderPipelineAsset renderPipelineAsset)
        {
            _injectedRenderPipeline = renderPipelineAsset;
        }

        public override void OnShowSettings()
        {
            Toggle.isOn = SettingsStore.IsAntialiasingUse;
        }

        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int(ConstantStore.UseAntialiasingSaveKey, isOn ? 1 : 0);

            _injectedRenderPipeline.msaaSampleCount = isOn ? OnMsaa : OffMsaa;
        }
    }
}