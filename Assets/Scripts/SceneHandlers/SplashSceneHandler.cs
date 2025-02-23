using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities;
using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using Managers;
using Stores;
using TMPro;
using UI;
using UI.Common;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace SceneHandlers
{
	public class SplashSceneHandler : MonoBehaviour
	{
		[Header("Resources")]
		[SerializeField] private UniversalRenderPipelineAsset universalRenderPipelineAsset;

		[SerializeField] private TMP_Text studioName;
		[SerializeField] private TMP_Text studioText;
		[SerializeField] private TMP_Text versionText;

		private void Start()
		{
#if UNITY_IOS
			versionText.text = $"v{Application.version}";
#elif UNITY_ANDROID
			versionText.text = $"v{Application.version}";
#endif
			AudioModule.OnInitialize();

			SettingsStore.GetLocalSettings();
			SetLocalGraphicSettings();

			Application.targetFrameRate = 120;

			FirebaseManager.Instance.CheckDependencies();

			Animation();
		}

		private void Animation()
		{
			FullFadeManager.Instance.FadeIn(ShowStudioName);
		}

		private void ShowStudioName()
		{
			var postActions = new FirebaseManager.CommonFirebaseCallback(
				onSuccess: () =>
				{
					studioName.DOText("Horang", 0.5f)
						.From(string.Empty)
						.SetEase(Ease.Linear)
						.OnComplete(ShowStudio);
				},
				onCanceled: OnCanceled,
				onFailed: OnFailed
			);

			FirebaseManager.Instance.AnonymouslyAuth(postActions);
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

		private void SetLocalGraphicSettings()
		{
			universalRenderPipelineAsset.msaaSampleCount = SettingsStore.IsAntialiasingUse ? 4 : 1;
			universalRenderPipelineAsset.renderScale = SettingsStore.IsHalfRenderScaleUse ? 0.7f : 1.0f;
		}
	}
}