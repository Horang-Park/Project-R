using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Managers;
using TMPro;
using UI;
using UI.Common;
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
			Application.targetFrameRate = 120;

			Animation();

			AudioModule.OnInitialize();
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
				.OnComplete(Auth);
		}

		private void Auth()
		{
			FirebaseManager.Instance.AnonymouslyAuth(new FirebaseManager.FirebasePostActions(onSuccess: ShowStudio, onCanceled: OnCanceled, onFailed: OnFailed));
		}

		private void ShowStudio()
		{
			studioText.DOFade(1.0f, 0.5f)
				.From(0.0f)
				.OnComplete(LoadMainScene);
			studioText.rectTransform.DOAnchorPosY(-45.0f, 0.5f)
				.From(new Vector2(0.0f, -55.0f));
		}

		private static async void LoadMainScene()
		{
			try
			{
				await UniTask.Delay(TimeSpan.FromMilliseconds(1000.0f));

				FullFadeManager.Instance.FadeOut(() =>
				{
					SceneManager.LoadSceneAsync(1).ToUniTask();
				});
			}
			catch (Exception e)
			{
				// ignored
			}
		}

		private static void OnCanceled()
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "Canceled to login. Please re-lunch game.",
				Title: "Login Failed.",
				RightButtonAction: () => { Application.Quit(-400); }));
		}

		private static void OnFailed(string exceptionMessage)
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "Failed to login. Please re-lunch game.",
				Title: "Login Failed.",
				RightButtonAction: () => { Application.Quit(-401); }));
		}

	}
}