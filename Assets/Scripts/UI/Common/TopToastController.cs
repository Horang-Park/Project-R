using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interfaces.UI;
using TMPro;
using UnityEngine;

namespace UI.Common
{
	public class TopToastController : BaseCommonUI, IGetTextComponents
	{
		public string Put
		{
			set => _data = value;
		}

		[Header("Toast Options")]
		[SerializeField] private float autoHideIntervalTime = 2000.0f;

		private TMP_Text _context;
		private CancellationTokenSource _cts = new();
		private string _data;

		public override void Show()
		{
			base.Show();

			_cts.Cancel();
			_cts.Dispose();
			_cts = new CancellationTokenSource();

			MainBackground.DOKill();
			MainBackground
				.DOAnchorPosY(-50.0f, 0.2f)
				.From(new Vector2(0.0f, 150.0f))
				.OnComplete(() => UniTask.RunOnThreadPool(AutoHide, cancellationToken: _cts.Token));
		}

		public void GetTextComponents()
		{
			_context = GetComponentInChildren<TMP_Text>();
		}

		protected override void Hide()
		{
			base.Hide();

			MainBackground.DOAnchorPosY(150.0f, 0.4f)
				.From(new Vector2(0.0f, -50.0f));
		}

		protected override void SetData()
		{
			_context.text = _data;
		}

		private async UniTask AutoHide()
		{
			await UniTask.Delay(TimeSpan.FromMilliseconds(autoHideIntervalTime), cancellationToken: _cts.Token);

			if (_cts.IsCancellationRequested)
			{
				return;
			}

			Hide();
		}
	}
}