using System;
using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Game
{
	public class LimitTimeController : MonoBehaviour
	{
		private TMP_Text _text;
		private int _currentTime;

		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
			
			_currentTime = ConstantStore.DefaultLimitTime;
		}

		private void Start()
		{
			Observable.EveryUpdate()
				.Where(_ => OneCycleRecordStore.IsCountdown.Value is false)
				.Where(_ => OneCycleRecordStore.IsTimeOver.Value is false)
				.Where(_ => FullFadeManager.Instance.IsFading.Value is false)
				.ThrottleFirst(TimeSpan.FromMilliseconds(1000.0f))
				.Subscribe(_ => TimeUpdater())
				.AddTo(gameObject);
		}

		private void TimeUpdater()
		{
			_text.text = _currentTime.ToString("00");

			_currentTime--;

			if (_currentTime is -1)
			{
				OneCycleRecordStore.IsTimeOver.Value = true;
			}
		}
	}
}