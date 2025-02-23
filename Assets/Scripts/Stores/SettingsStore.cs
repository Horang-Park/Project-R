using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities;
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
            var isFirstLaunch = GetPlayerPrefs.Int(ConstantStore.IsFirstLaunchSaveKey);

            if (isFirstLaunch.Equals(int.MaxValue) || isFirstLaunch.Equals(1))
            {
                SetPlayerPrefs.Int(ConstantStore.IsFirstLaunchSaveKey, 0);

                // 기본 값 세팅
                SetPlayerPrefs.Float(ConstantStore.BgmVolumeSaveKey, 0.8f);
                SetPlayerPrefs.Int(ConstantStore.BgmOnOffSaveKey, 1);
                SetPlayerPrefs.Float(ConstantStore.SfxVolumeSaveKey, 0.8f);
                SetPlayerPrefs.Int(ConstantStore.SfxOnOffSaveKey, 1);

                SetPlayerPrefs.Int(ConstantStore.UseHalfRenderScaleSaveKey, 0);
                SetPlayerPrefs.Int(ConstantStore.UseAntialiasingSaveKey, 1);
                SetPlayerPrefs.Int(ConstantStore.UsePostProcessingSaveKey, 1);
            }

            BgmVolume = GetPlayerPrefs.Float(ConstantStore.BgmVolumeSaveKey);
            IsBgmUse = GetPlayerPrefs.Int(ConstantStore.BgmOnOffSaveKey).Equals(1);
            SfxVolume = GetPlayerPrefs.Float(ConstantStore.SfxVolumeSaveKey);
            IsSfxUse = GetPlayerPrefs.Int(ConstantStore.SfxOnOffSaveKey).Equals(1);

            Log.Print($"bgm volume: {BgmVolume} / is bgm use: {IsBgmUse} / sfx volume: {SfxVolume} / is sfx use: {IsSfxUse}");

            IsHalfRenderScaleUse = GetPlayerPrefs.Int(ConstantStore.UseHalfRenderScaleSaveKey).Equals(1);
            IsAntialiasingUse = GetPlayerPrefs.Int(ConstantStore.UseAntialiasingSaveKey).Equals(1);
            IsPostProcessingUse = GetPlayerPrefs.Int(ConstantStore.UsePostProcessingSaveKey).Equals(1);

            Log.Print($"is half render scale use: {IsHalfRenderScaleUse} / is antialiasing use: {IsAntialiasingUse} / is post processing use: {IsPostProcessingUse}");

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