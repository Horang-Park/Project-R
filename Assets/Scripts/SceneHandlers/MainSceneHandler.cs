using UI;
using UnityEngine;

namespace SceneHandlers
{
	public class MainSceneHandler : MonoBehaviour
	{
		private void Start()
		{
			FullFadeManager.Instance.FadeIn();
		}
	}
}