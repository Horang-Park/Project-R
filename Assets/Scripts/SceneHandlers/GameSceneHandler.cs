using Horang.HorangUnityLibrary.Modules.CameraModule;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
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

			var usePostProcessing = GetPlayerPrefs.Int("Use Post Processing");

			CameraModule.GetCamera("Main Camera").GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = usePostProcessing.Equals(1);
		}

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}

		private void OnDestroy()
		{
			CameraModule.Dispose();
		}
	}
}