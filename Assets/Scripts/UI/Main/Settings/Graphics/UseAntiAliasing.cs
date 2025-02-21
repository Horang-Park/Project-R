using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
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

        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int("Use Antialiasing", isOn ? 1 : 0);

            _injectedRenderPipeline.msaaSampleCount = isOn ? OnMsaa : OffMsaa;
        }
    }
}