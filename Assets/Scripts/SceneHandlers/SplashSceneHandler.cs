using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities;
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

			FirebaseManager.Instance.CheckDependencies();

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

		private static async void LoadMainScene()
		{
			try
			{
				await UniTask.Delay(TimeSpan.FromMilliseconds(1000.0f));

				FullFadeManager.Instance.FadeOut(() =>
				{
					var postActions = new FirebaseManager.FirebasePostActions(
						onSuccess: () => SceneManager.LoadSceneAsync(1).ToUniTask(),
						onCanceled: OnCanceled,
						onFailed: OnFailed
					);

					FirebaseManager.Instance.AnonymouslyAuth(postActions);
				});
			}
			catch (Exception e)
			{
				Log.Print($"Auth throw an exception. -> {e.Message}", LogPriority.Exception);
			}
		}

		private static void OnCanceled()
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "로그인이 취소되었습니다. 게임을 재기동 해주십시오.",
				Title: "로그인",
				RightButtonAction: () => { Application.Quit(-400); },
				UseOneButton: true));
		}

		private static void OnFailed(string exceptionMessage)
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "로그인을 실패했습니다. 게임을 재기동 해주십시오.",
				Title: "로그인",
				RightButtonAction: () => { Application.Quit(-401); },
				UseOneButton: true));
		}
	}
}