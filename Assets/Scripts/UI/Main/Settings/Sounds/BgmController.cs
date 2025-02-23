using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;

namespace UI.Main.Settings.Sounds
{
    public class BgmController : BaseSoundController
    {
        public override void OnShowSettings()
        {
            Slider.value = SettingsStore.BgmVolume;
            OnOff.isOn = SettingsStore.IsBgmUse;
        }

        protected override void SetVolume(float volume)
        {
            AudioModule.Volume("slider", volume);

            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.BGM, volume);

            SetPlayerPrefs.Float(ConstantStore.BgmVolumeSaveKey, volume);
        }

        protected override void OnSound(bool isOn)
        {
            SetPlayerPrefs.Int(ConstantStore.BgmOnOffSaveKey, isOn ? 1 : 0);

            if (isOn)
            {
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.BGM);

                return;
            }

            AudioModule.MuteByCategory(AudioDataType.AudioPlayType.BGM);
        }
    }
}