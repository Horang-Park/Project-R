using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public enum CommonUIVisibility
	{
		Showing = 0b0010,
		Show = 0b0011,
		Hiding = 0b1000,
		Hide = 0b1100,
	}
	
	public abstract class BaseCommonUI : MonoBehaviour
	{
		protected RectTransform MainBackground;
		protected CommonUIVisibility CommonUIVisibility = CommonUIVisibility.Hide;
		
		private CanvasGroup _canvasGroup;

		protected abstract void SetData();

		public void Initialize()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			MainBackground = transform.GetChild(0).GetComponent<RectTransform>();

			_canvasGroup.interactable = false;
			_canvasGroup.blocksRaycasts = false;
		}

		public virtual void Show()
		{
			SetData();

			_canvasGroup.DOFade(1.0f, 0.4f)
				.OnPlay(() =>
				{
					_canvasGroup.blocksRaycasts = true;
					CommonUIVisibility = CommonUIVisibility.Showing;
				})
				.OnComplete(() =>
				{
					_canvasGroup.interactable = true;
					CommonUIVisibility = CommonUIVisibility.Show;
				})
				.From(0.0f);
		}

		protected virtual void Hide()
		{
			_canvasGroup.DOFade(0.0f, 0.2f)
				.OnPlay(() =>
				{
					_canvasGroup.interactable = false;
					CommonUIVisibility = CommonUIVisibility.Hiding;
				})
				.OnComplete(() =>
				{
					_canvasGroup.blocksRaycasts = false;

					CommonUIVisibility = CommonUIVisibility.Hide;
				})
				.From(1.0f);
		}
	}
}