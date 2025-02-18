using System.Globalization;
using DG.Tweening;
using Horang.HorangUnityLibrary.Utilities;
using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
	public class ScoreController : MonoBehaviour
	{
		private TMP_Text _text;
		private int _currentValue;

		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void Start()
		{
			OneCycleRecordStore.Score
				.Subscribe(ScoreUpdate)
				.AddTo(this);
		}

		private void ScoreUpdate(int score)
		{
			_text.DOKill();
			_text.DOCounter(_currentValue, score, 0.3f, true, CultureInfo.CurrentCulture)
				.SetEase(Ease.OutQuint)
				.OnComplete(() => _currentValue = score)
				.OnKill(() =>
				{
					_text.text = score.ToString("# ##0");
					_currentValue = score;
				});
		}
	}
}