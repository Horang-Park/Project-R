using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public enum CommonUIVisibility
	{
		Invalid = 0b0000,
		Showing = 0b0010,
		Show = 0b0011,
		Hiding = 0b1000,
		Hide = 0b1100,
	}
	
	public abstract class BaseCommonUI : MonoBehaviour
	{
		
		protected RectTransform MainBackground;
		protected CommonUIVisibility CommonUIVisibility = CommonUIVisibility.Invalid;
		
		private CanvasGroup _canvasGroup;

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			MainBackground = transform.GetChild(0).GetComponent<RectTransform>();
		}

		private void Start()
		{
			_canvasGroup.interactable = false;
			_canvasGroup.blocksRaycasts = false;
		}

		public virtual void Show()
		{
			if ((int)CommonUIVisibility >> 1 == 0b0001)
			{
				return;
			}
			
			_canvasGroup.DOFade(1.0f, 0.5f)
				.OnPlay(() =>
				{
					_canvasGroup.blocksRaycasts = true;
					CommonUIVisibility = CommonUIVisibility.Showing;
				})
				.OnComplete(() =>
				{
					_canvasGroup.interactable = true;
					CommonUIVisibility = CommonUIVisibility.Show;
				});
		}

		public virtual void Hide()
		{
			if ((int)CommonUIVisibility >> 3 == 0b0001)
			{
				return;
			}
			
			_canvasGroup.DOFade(0.0f, 0.3f)
				.OnPlay(() =>
				{
					_canvasGroup.interactable = false;
					CommonUIVisibility = CommonUIVisibility.Hiding;
				})
				.OnComplete(() =>
				{
					_canvasGroup.blocksRaycasts = false;
					
					CommonUIVisibility = CommonUIVisibility.Hide;
				});
		}
	}
}