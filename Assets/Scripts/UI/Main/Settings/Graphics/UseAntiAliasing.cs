using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI.Main.Settings.Graphics
{
    public class UseAntiAliasing : MonoBehaviour
    {
        private Toggle _toggle;
        private UniversalRenderPipelineAsset _injectedRenderPipeline;

        private const int OnMsaa = 4; // 4x
        private const int OffMsaa = 1; // Disabled

        public void Initialize(UniversalRenderPipelineAsset renderPipelineAsset)
        {
            _injectedRenderPipeline = renderPipelineAsset;

            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(Antialiasing);
        }

        private void Antialiasing(bool isOn)
        {
            SetPlayerPrefs.Int("Use Antialiasing", isOn ? 1 : 0);

            _injectedRenderPipeline.msaaSampleCount = isOn ? OnMsaa : OffMsaa;
        }
    }
}