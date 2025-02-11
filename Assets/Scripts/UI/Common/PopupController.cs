using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public class PopupController : BaseCommonUI
	{
		public override void Show()
		{
			base.Show();
			
			if ((int)CommonUIVisibility >> 1 == 0b0001)
			{
				return;
			}

			MainBackground.DOAnchorPosY(0.0f, 0.3f)
				.From(new Vector2(0.0f, -40.0f));
		}

		public override void Hide()
		{
			base.Hide();
			
			if ((int)CommonUIVisibility >> 3 == 0b0001)
			{
				return;
			}
			
			MainBackground.DOAnchorPosY(-40.0f, 0.5f)
				.From(new Vector2(0.0f, 0.0f));
		}
	}
}