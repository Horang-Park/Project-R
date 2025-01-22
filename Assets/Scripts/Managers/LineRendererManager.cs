using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Managers
{
	public class LineRendererManager : MonoBehaviour
	{
		private LineRenderer _lineRenderer;

		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();

			gameObject.hideFlags = HideFlags.HideInHierarchy;
		}

		private void Start()
		{
			DragInputManager.Instance.onPointerDownPositionEventProvider
				.Subscribe(pos => SetPosition(0, pos))
				.AddTo(this);
			
			DragInputManager.Instance.onDragPositionEventProvider
				.Subscribe(pos => SetPosition(1, pos))
				.AddTo(this);

			Observable.EveryUpdate()
				.Where(_ => Input.GetMouseButtonUp(0))
				.Subscribe(_ => OnMouseUpInitialize())
				.AddTo(this);
		}

		private void OnDestroy()
		{
			_lineRenderer.DOKill();
		}

		private void SetPosition(int index, Vector2 position)
		{
			_lineRenderer.DOKill();
			ResetColor();
			
			_lineRenderer.SetPosition(index, position);
		}

		private void OnMouseUpInitialize()
		{
			_lineRenderer.DOKill();
			_lineRenderer.DOColor(new Color2(Color.white, Color.white), new Color2(Color.clear, Color.clear), 0.2f)
				.OnComplete(() =>
				{
					for (var index = 0; index < _lineRenderer.positionCount; index++)
					{
						_lineRenderer.SetPosition(index, Vector3.zero);
					}

					ResetColor();
				});
		}

		private void ResetColor()
		{
			_lineRenderer.startColor = Color.white;
			_lineRenderer.endColor = Color.white;
		}
	}
}