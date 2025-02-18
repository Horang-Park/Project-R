using System;
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