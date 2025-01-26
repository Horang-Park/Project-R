using Cysharp.Threading.Tasks;
using DG.Tweening;
using Managers;
using Stores;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Game.GameOver
{
	public class GameOverUIManager : MonoBehaviour
	{
		private CanvasGroup _canvasGroup;
		private TMP_Text _score;
		private Button _back;

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			
			_score = GetComponentsInChildren<TMP_Text>()[1];

			var buttons = GetComponentsInChildren<Button>();
			_back = buttons[0];
		}

		private void Start()
		{
			_back.OnClickAsObservable()
				.Subscribe(_ => Back())
				.AddTo(gameObject);
			
			OneCycleRecordStore.IsTimeOver
				.Where(isTimeOver => isTimeOver)
				.Subscribe(_ => Show())
				.AddTo(this);
		}

		private void Show()
		{
			_canvasGroup.interactable = true;
			_canvasGroup.blocksRaycasts = true;
			_canvasGroup.DOFade(1.0f, 1.0f)
				.OnComplete(ShowScore);
		}

		private void ShowScore()
		{
			_score.DOFade(1.0f, 1.0f);
			_score.rectTransform.DOAnchorPosY(-400.0f, 1.0f)
				.From(new Vector2(0.0f, -430.0f));
			_score.DOCounter(0, OneCycleRecordStore.Score.Value, 1.5f)
				.OnComplete(ShowButtons);
		}

		private void ShowButtons()
		{
			_back.GetComponent<CanvasGroup>().DOFade(1.0f, 1.0f);
			_back.GetComponent<RectTransform>().DOAnchorPosY(-450.0f, 1.0f)
				.From(new Vector2(0.0f, -480.0f));
		}
		
		private void Back()
		{
			DragInputManager.Instance.DisposeEvents();
			
			FullFadeManager.Instance.FadeOut(() =>
			{
				SceneManager.LoadSceneAsync(1).ToUniTask();
			});
		}
	}
}