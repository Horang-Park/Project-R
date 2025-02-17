using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Firebase.Extensions;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities;
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
				.OnPlay(DoAnonymouslyAuth)
				.OnComplete(ShowStudio);
		}

		private void ShowStudio()
		{
			studioText.DOFade(1.0f, 0.5f)
				.From(0.0f);
			studioText.rectTransform.DOAnchorPosY(-45.0f, 0.5f)
				.From(new Vector2(0.0f, -55.0f));
		}

		private void DoAnonymouslyAuth()
		{
			var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

			auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
			{
				if (task.IsCanceled)
				{
					Log.Print("SignInAnonymouslyAsync auth canceled.");

					CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
						Context: "Canceled to login. Please re-lunch game.",
						Title: "Login Failed.",
						RightButtonAction: () => { Application.Quit(-400); }));

					return;
				}

				if (task.IsFaulted)
				{
					Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);

					CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
						Context: "Failed to login. Please re-lunch game.",
						Title: "Login Failed.",
						RightButtonAction: () => { Application.Quit(-401); }));

					return;
				}

				var result = task.Result;

				Log.Print($"Success: {result.User.DisplayName} / {result.User.UserId}");

				LoadMainScene();
			});
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