using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;

namespace UI.Main.Settings.Sounds
{
    public class SfxController : BaseSoundController
    {
        public override void OnShowSettings()
        {
            Slider.value = SettingsStore.SfxVolume;
            OnOff.isOn = SettingsStore.IsSfxUse;
        }

        protected override void SetVolume(float volume)
        {
            AudioModule.Volume("slider", volume);

            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.OneshotSfx, volume);
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.LoopSfx, volume);

            SetPlayerPrefs.Float(ConstantStore.SfxVolumeSaveKey, volume);
        }

        protected override void OnSound(bool isOn)
        {
            SetPlayerPrefs.Int(ConstantStore.SfxOnOffSaveKey, isOn ? 1 : 0);

            if (isOn)
            {
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.OneshotSfx);
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.LoopSfx);

                return;
            }

            AudioModule.MuteByCategory(AudioDataType.AudioPlayType.OneshotSfx);
            AudioModule.MuteByCategory(AudioDataType.AudioPlayType.LoopSfx);
        }
    }
}