using Horang.HorangUnityLibrary.Modules.CameraModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;
using UnityEngine.Rendering.Universal;

namespace UI.Main.Settings.Graphics
{
    public class UsePostProcessing : BaseGraphicController
    {
        public override void OnShowSettings()
        {
            Toggle.isOn = SettingsStore.IsPostProcessingUse;
        }

        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int(ConstantStore.UsePostProcessingSaveKey, isOn ? 1 : 0);

            CameraModule.GetCamera("Main Camera").GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = isOn;
        }
    }
}