using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Common
{
	public class TopToastController : BaseCommonUI
	{
		[SerializeField] private bool useAutoHide = true;
		[SerializeField] private float intervalTime = 2000.0f;

		private CancellationTokenSource _cts = new();
		
		public override void Show()
		{
			base.Show();

			_cts.Cancel();
			_cts.Dispose();
			_cts = new CancellationTokenSource();

			MainBackground.DOAnchorPosY(-50.0f, 0.3f)
				.From(new Vector2(0.0f, 0.0f))
				.OnComplete(() =>
				{
					if (useAutoHide is false)
					{
						return;
					}

					UniTask.RunOnThreadPool(() => AutoHide(_cts.Token));
				});
		}

		public override void Hide()
		{
			base.Hide();
			
			if ((int)CommonUIVisibility >> 3 == 0b0001)
			{
				return;
			}
			
			MainBackground.DOAnchorPosY(0.0f, 0.5f)
				.From(new Vector2(0.0f, -50.0f));
		}

		private async UniTask AutoHide(CancellationToken token)
		{
			await UniTask.Delay(TimeSpan.FromMilliseconds(intervalTime), cancellationToken: token);

			if (token.IsCancellationRequested)
			{
				return;
			}
			
			Hide();
		}
	}
}