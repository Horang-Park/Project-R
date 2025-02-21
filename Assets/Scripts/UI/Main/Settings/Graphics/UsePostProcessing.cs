using Horang.HorangUnityLibrary.Modules.CameraModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using UnityEngine.Rendering.Universal;

namespace UI.Main.Settings.Graphics
{
    public class UsePostProcessing : BaseGraphicController
    {
        protected override void OnGraphicSetting(bool isOn)
        {
            SetPlayerPrefs.Int("Use Post Processing", isOn ? 1 : 0);

            CameraModule.GetCamera("Main Camera").GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = isOn;
        }
    }
}