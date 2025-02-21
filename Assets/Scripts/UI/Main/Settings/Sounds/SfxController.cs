using Horang.HorangUnityLibrary.Modules.AudioModule;

namespace UI.Main.Settings.Sounds
{
    public class SfxController : BaseSoundController
    {
        protected override void SetVolume(float volume)
        {
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.OneshotSfx, volume);
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.LoopSfx, volume);
        }

        protected override void OnSound(bool isOn)
        {
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