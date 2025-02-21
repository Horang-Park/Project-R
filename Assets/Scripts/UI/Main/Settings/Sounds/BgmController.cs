using Horang.HorangUnityLibrary.Modules.AudioModule;

namespace UI.Main.Settings.Sounds
{
    public class BgmController : BaseSoundController
    {
        protected override void SetVolume(float volume)
        {
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.BGM, volume);
        }

        protected override void OnSound(bool isOn)
        {
            if (isOn)
            {
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.BGM);

                return;
            }

            AudioModule.MuteByCategory(AudioDataType.AudioPlayType.BGM);
        }
    }
}