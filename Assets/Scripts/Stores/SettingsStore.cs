using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;

namespace Stores
{
    public struct SettingsStore
    {
        public static float BgmVolume;
        public static bool IsBgmUse;
        public static float SfxVolume;
        public static bool IsSfxUse;

        public static bool IsAntialiasingUse;
        public static bool IsHalfRenderScaleUse;
        public static bool IsPostProcessingUse;

        public static void GetLocalSettings()
        {
            BgmVolume = GetPlayerPrefs.Float(ConstantStore.BgmVolumeSaveKey);
            IsBgmUse = GetPlayerPrefs.Int(ConstantStore.BgmOnOffSaveKey).Equals(1);
            SfxVolume = GetPlayerPrefs.Float(ConstantStore.SfxVolumeSaveKey);
            IsSfxUse = GetPlayerPrefs.Int(ConstantStore.SfxOnOffSaveKey).Equals(1);

            IsAntialiasingUse = GetPlayerPrefs.Int(ConstantStore.UseAntialiasingSaveKey).Equals(1);
            IsHalfRenderScaleUse = GetPlayerPrefs.Int(ConstantStore.UseHalfRenderScaleSaveKey).Equals(1);
            IsPostProcessingUse = GetPlayerPrefs.Int(ConstantStore.UsePostProcessingSaveKey).Equals(1);

            SetLocalAudioSettings();
        }

        private static void SetLocalAudioSettings()
        {
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.BGM, BgmVolume);

            if (IsBgmUse)
            {
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.BGM);
            }
            else
            {
                AudioModule.MuteByCategory(AudioDataType.AudioPlayType.BGM);
            }

            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.OneshotSfx, SfxVolume);
            AudioModule.VolumeByCategory(AudioDataType.AudioPlayType.LoopSfx, SfxVolume);

            if (IsSfxUse)
            {
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.OneshotSfx);
                AudioModule.UnmuteByCategory(AudioDataType.AudioPlayType.LoopSfx);
            }
            else
            {
                AudioModule.MuteByCategory(AudioDataType.AudioPlayType.OneshotSfx);
                AudioModule.MuteByCategory(AudioDataType.AudioPlayType.LoopSfx);
            }
        }
    }
}