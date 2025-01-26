using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Horang.HorangUnityLibrary.Managers.Module;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandlers
{
	public class SplashSceneHandler : MonoBehaviour
	{
		[SerializeField] private TMP_Text studioName;
		[SerializeField] private TMP_Text studioText;

		private void Start()
		{
			ModuleManager.Instance.RegisterModule(new AudioModule());

			Application.targetFrameRate = 120;
			
			Animation();
		}

		private void Animation()
		{
			FullFadeManager.Instance.FadeIn(ShowStudioName);
		}

		private void ShowStudioName()
		{
			studioName.DOText("Horang", 1.0f)
				.From(string.Empty)
				.SetEase(Ease.Linear)
				.OnComplete(ShowStudio);
		}

		private void ShowStudio()
		{
			studioText.DOFade(1.0f, 0.5f)
				.From(0.0f)
				.OnComplete(LoadMainScene);
			studioText.rectTransform.DOAnchorPosY(-45.0f, 0.5f)
				.From(new Vector2(0.0f, -55.0f));
		}

		private async void LoadMainScene()
		{
			await UniTask.Delay(TimeSpan.FromMilliseconds(1000.0f));
			
			FullFadeManager.Instance.FadeOut(() =>
			{
				SceneManager.LoadSceneAsync(1).ToUniTask();
			});
		}
	}
}