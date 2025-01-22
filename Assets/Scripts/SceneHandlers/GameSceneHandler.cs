using System;
using Horang.HorangUnityLibrary.Managers.Module;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using UnityEngine;

namespace SceneHandlers
{
	public class GameSceneHandler : MonoBehaviour
	{
		private void Awake()
		{
			ModuleManager.Instance.RegisterModule(new CameraModule());
			ModuleManager.Instance.RegisterModule(new AudioModule());

			Application.targetFrameRate = 120;
		}
	}
}