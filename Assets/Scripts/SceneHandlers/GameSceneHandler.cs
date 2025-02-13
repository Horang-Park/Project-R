using Horang.HorangUnityLibrary.Modules.CameraModule;
using Stores;
using UI;
using UnityEngine;

namespace SceneHandlers
{
	public class GameSceneHandler : MonoBehaviour
	{
		private void Awake()
		{
			CameraModule.OnInitialize();

			OneCycleRecordStore.Flush();
		}

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}
	}
}