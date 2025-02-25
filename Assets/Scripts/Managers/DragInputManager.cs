using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Modules.CameraModule;
using Stores;
using UI;
using UniRx;
using UnityEngine;

namespace Managers
{
	public class DragInputManager : MonoSingleton<DragInputManager>
	{
		[Header("Event Providers")]
		public Vector2ReactiveProperty onPointerDownPositionEventProvider = new();
		public Vector2ReactiveProperty onDragPositionEventProvider = new();
		public Vector2ReactiveProperty onPointerUpPositionEventProvider = new();
		
		private Camera _mainCamera;
		private bool _blockInput;

		public void DisposeEvents()
		{
			onPointerDownPositionEventProvider.Dispose();
			onDragPositionEventProvider.Dispose();
			onPointerUpPositionEventProvider.Dispose();

			onPointerDownPositionEventProvider = new Vector2ReactiveProperty();
			onDragPositionEventProvider = new Vector2ReactiveProperty();
			onPointerUpPositionEventProvider = new Vector2ReactiveProperty();
		}

		private void Start()
		{
			_mainCamera = CameraModule.GetCamera("Main Camera");
			
			Observable.EveryUpdate()
				.Where(_ => Input.GetMouseButtonDown(0) && _blockInput is false)
				.Subscribe(_ => MouseButtonDownUpdater())
				.AddTo(gameObject);

			Observable.EveryUpdate()
				.Where(_ => Input.GetMouseButton(0) && _blockInput is false)
				.Subscribe(_ => MouseButtonDragUpdater())
				.AddTo(gameObject);

			Observable.EveryUpdate()
				.Where(_ => Input.GetMouseButtonUp(0) && _blockInput is false)
				.Subscribe(_ => MouseButtonUpUpdater())
				.AddTo(gameObject);

			OneCycleRecordStore.IsCountdown
				.Subscribe(isCountdown =>
				{
					_blockInput = isCountdown;

					if (isCountdown)
					{
						return;
					}

					OneCycleRecordStore.IsTimeOver
						.Subscribe(isTimeOver => _blockInput = isTimeOver)
						.AddTo(this);

					FullFadeManager.Instance.IsFading
						.Subscribe(isFading => _blockInput = isFading)
						.AddTo(this);
				})
				.AddTo(this);
		}

		private void MouseButtonDownUpdater()
		{
			onPointerDownPositionEventProvider.Value = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}

		private void MouseButtonDragUpdater()
		{
			onDragPositionEventProvider.Value = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}

		private void MouseButtonUpUpdater()
		{
			onPointerUpPositionEventProvider.Value = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}