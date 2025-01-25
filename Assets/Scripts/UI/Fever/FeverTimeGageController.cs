using System;
using DG.Tweening;
using Horang.HorangUnityLibrary.Utilities;
using Stores;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fever
{
	public class FeverTimeGageController : MonoBehaviour
	{
		public float Value
		{
			set
			{
				if (value > 1.0f)
				{
					Log.Print("세팅할 값은 1.0 이하로 해야함.", LogPriority.Exception);

					throw new OverflowException();
				}

				_foreground.fillAmount = value;
			}
		}

		public float AddValue
		{
			set
			{
				if (value > 1.0f)
				{
					Log.Print("더할 값은 1.0 이하로 해야함.", LogPriority.Exception);

					throw new OverflowException();
				}

				_foreground.DOKill();
				
				if (_foreground.fillAmount + value >= 1.0f)
				{
					_foreground.fillAmount = 1.0f;

					return;
				}
				
				_foreground.fillAmount += value;
			}
		}

		public bool IsFeverTime => _isFeverTime;

		private Image _foreground;
		private bool _isFeverTime;

		private void Awake()
		{
			_foreground = transform.GetChild(1).GetComponent<Image>();
		}

		private void Start()
		{
			Observable.EveryUpdate()
				.Where(_ => _isFeverTime)
				.Where(_ => _foreground.fillAmount > 0.0f)
				.Subscribe(_ => Timer())
				.AddTo(gameObject);

			OneCycleRecordStore.KilledEnemiesForFeverTime
				.Subscribe(KilledEnemiesForFeverTimeUpdater)
				.AddTo(gameObject);
		}

		private void Timer()
		{
			// var target = _foreground.fillAmount - ConstantStore.FeverTimeDecreaseStep * OneCycleRecordStore.CurrentFeverMultiplier.Value;

			_foreground.fillAmount -= ConstantStore.FeverTimeDecreaseStep * OneCycleRecordStore.CurrentFeverMultiplier.Value * Time.deltaTime;
			
			if (_foreground.fillAmount > 0.0f || _isFeverTime is false)
			{
				return;
			}

			_isFeverTime = false;

			OneCycleRecordStore.CurrentFeverMultiplier.Value = ConstantStore.DefaultFeverMultiplier;

			// _foreground.DOFillAmount(target, 0.987f)
			// 	.SetEase(Ease.Linear)
			// 	.OnComplete(() =>
			// 	{
			// 		if (_foreground.fillAmount > 0.0f || _isFeverTime is false)
			// 		{
			// 			return;
			// 		}
			//
			// 		_isFeverTime = false;
			//
			// 		OneCycleRecordStore.CurrentFeverMultiplier.Value = ConstantStore.DefaultFeverMultiplier;
			// 	});
		}

		private void KilledEnemiesForFeverTimeUpdater(int count)
		{
			var value = (float)count / ConstantStore.RequireKilledEnemyCountToSetFeverMode;

			if (value < 1.0f && _isFeverTime is false)
			{
				_foreground.DOFillAmount(value, 0.3f);

				return;
			}

			_foreground.DOFillAmount(1.0f, 0.5f)
				.OnComplete(() =>
				{
					_isFeverTime = true;

					OneCycleRecordStore.KilledEnemiesForFeverTime.Value = 0;
				});
		}
	}
}