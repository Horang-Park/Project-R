using UI;
using UI.Common;
using UnityEngine;

namespace SceneHandlers
{
	public class MainSceneHandler : MonoBehaviour
	{
		private int toastTestIndex;

		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				CommonCanvasManager.Instance.ShowToast($"토스트 테스트 {toastTestIndex}");

				toastTestIndex++;
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(Context: $"asdfasdf {toastTestIndex}", Title: "Test"));
			}
		}
	}
}