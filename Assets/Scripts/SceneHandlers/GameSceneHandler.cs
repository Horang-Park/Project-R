using Horang.HorangUnityLibrary.Managers.Module;
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
			ModuleManager.Instance.RegisterModule(new CameraModule());
			
			OneCycleRecordStore.Flush();
		}

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}

		private void OnDestroy()
		{
			ModuleManager.Instance.UnregisterModule(typeof(CameraModule));
		}
	}
}