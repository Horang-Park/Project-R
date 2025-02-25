using System;
using Firebase.Auth;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using Managers;
using Stores;
using UI;
using UI.Common;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SceneHandlers
{
	public class MainSceneHandler : MonoBehaviour
	{
		private void Awake()
		{
			CameraModule.OnInitialize();

			if (AudioModule.IsAudioPlaying("main_bgm"))
			{
				return;
			}

			AudioModule.Play("main_bgm");
		}

		private void OnDestroy()
		{
			CameraModule.Dispose();
		}

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();

			CameraModule.GetCamera("Main Camera").GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = SettingsStore.IsPostProcessingUse;

			if (FirebaseManager.Instance.IsUserDisplayNameNullOrEmpty)
			{
				CommonCanvasManager.Instance.ShowInputFieldPopup(new InputFieldPopupController.Data(
					Context: "닉네임을 설정해 주십시오.",
					ButtonAction: Send
				));
			}
		}

		private static void Send(string inputFieldText)
		{
			var profile = new UserProfile
			{
				DisplayName = inputFieldText,
				PhotoUrl = new Uri("https://picsum.photos/50/50"),
			};

			FirebaseManager.Instance.SetUserProfile(profile, new FirebaseManager.CommonFirebaseCallback(
				onSuccess: () => { OnSuccess(profile); },
				onCanceled: OnCanceled,
				onFailed: OnFailed
			));
		}

		private static void OnSuccess(UserProfile userProfile)
		{
			CommonCanvasManager.Instance.ShowToast($"닉네임이 [{userProfile.DisplayName}](으)로 변경되었습니다.");

			FirebaseManager.Instance.AddUser(userProfile.DisplayName, userProfile.PhotoUrl.ToString());
		}

		private static void OnCanceled()
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "닉네임 설정이 취소되었습니다.\n게임을 재기동 해주십시오.",
				Title: "닉네임 설정",
				RightButtonAction: () => { Application.Quit(-400); },
				UseOneButton: true));
		}

		private static void OnFailed(string exceptionMessage)
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "닉네임 설정을 실패했습니다.\n게임을 재기동 해주십시오.",
				Title: "닉네임 설정",
				RightButtonAction: () => { Application.Quit(-401); },
				UseOneButton: true));
		}
	}
}