using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class FullFadeManager : MonoBehaviour
	{
		public static FullFadeManager Instance;
		
		public readonly BoolReactiveProperty IsFading = new();
		
		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();
			
			Instance = this;
		}

		public void FadeIn(Action onComplete = null)
		{
			IsFading.Value = true;
			_image.raycastTarget = true;
			
			_image.DOFade(0.0f, 0.5f)
				.From(1.0f)
				.OnComplete(() =>
				{
					_image.raycastTarget = false;
					
					onComplete?.Invoke();

					IsFading.Value = false;
				});
		}

		public void FadeOut(Action onComplete = null)
		{
			IsFading.Value = true;
			_image.raycastTarget = true;
			
			_image.DOFade(1.0f, 0.5f)
				.From(0.0f)
				.OnComplete(() =>
				{
					onComplete?.Invoke();

					IsFading.Value = false;
				});
		}
	}
}
