using Cysharp.Threading.Tasks;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Main
{
	public class GameStartButtonController : MonoBehaviour
	{
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void Start()
		{
			_button.OnClickAsObservable()
				.Subscribe(_ => LoadScene())
				.AddTo(gameObject);
		}

		private void LoadScene()
		{
			FullFadeManager.Instance.FadeOut(() =>
			{
				AudioModule.Stop("main_bgm");

				SceneManager.LoadSceneAsync(2).ToUniTask();
			});
		}
	}
}