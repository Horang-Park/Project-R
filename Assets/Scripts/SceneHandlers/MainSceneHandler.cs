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
					Context: "Set your display user name.",
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
				onSuccess: () => { CommonCanvasManager.Instance.ShowToast($"Display name set as [{inputFieldText}]"); },
				onCanceled: OnCanceled,
				onFailed: OnFailed
			));
		}

		private static void OnCanceled()
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "Canceled to update profile. Please re-lunch game.",
				Title: "Update Profile Canceled.",
				RightButtonAction: () => { Application.Quit(-400); },
				UseOneButton: true));
		}

		private static void OnFailed(string exceptionMessage)
		{
			CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
				Context: "Failed to update profile. Please re-lunch game.",
				Title: "Update Profile Failed.",
				RightButtonAction: () => { Application.Quit(-401); },
				UseOneButton: true));
		}
	}
}