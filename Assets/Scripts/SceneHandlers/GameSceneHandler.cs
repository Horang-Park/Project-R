using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Stores;
using UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SceneHandlers
{
	public class GameSceneHandler : MonoBehaviour
	{
		private void Awake()
		{
			CameraModule.OnInitialize();

			CameraModule.GetCamera("Main Camera").GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = SettingsStore.IsPostProcessingUse;
		}

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}

		private void OnDestroy()
		{
			CameraModule.Dispose();

			AudioModule.Stop("in_game_bgm");
		}
	}
}