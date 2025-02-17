using Firebase.Auth;
using Managers;
using UI;
using UI.Common;
using UnityEngine;

namespace SceneHandlers
{
	public class MainSceneHandler : MonoBehaviour
	{
		private void Start()
		{
			FullFadeManager.Instance.FadeIn();

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
			};

			FirebaseManager.Instance.SetUserProfile(profile, new FirebaseManager.FirebasePostActions(
				onSuccess: () => { CommonCanvasManager.Instance.ShowToast($"닉네임이 [{inputFieldText}](으)로 변경되었습니다."); },
				onCanceled: OnCanceled,
				onFailed: OnFailed
			));
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