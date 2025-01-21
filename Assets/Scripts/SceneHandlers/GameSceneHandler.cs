using System;
using Horang.HorangUnityLibrary.Managers.Module;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using UnityEngine;

namespace SceneHandlers
{
	public class GameSceneHandler : MonoBehaviour
	{
		private void Awake()
		{
			ModuleManager.Instance.RegisterModule(new CameraModule());
		}
	}
}