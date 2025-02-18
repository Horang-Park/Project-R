using Managers;
using Stores;
using UniRx;
using UnityEngine;

namespace Behaviour
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float shotPower = 5.0f;
		
		private Rigidbody2D _rigidbody2D;
		private Vector2 _mouseDownPosition;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			DragInputManager.Instance.onPointerDownPositionEventProvider
				.Subscribe(val => { _mouseDownPosition = val; })
				.AddTo(this);

			DragInputManager.Instance.onPointerUpPositionEventProvider
				.Subscribe(AddForce)
				.AddTo(this);

			OneCycleRecordStore.IsTimeOver
				.Where(b => b)
				.Subscribe(_ => OnTimeOver());
		}

		private void AddForce(Vector2 mouseUpPosition)
		{
			var forceVector = (_mouseDownPosition - mouseUpPosition);

			_rigidbody2D.linearVelocity = forceVector * shotPower;
			
			// SoundManager.Instance.Play("Shoot");
		}

		private void OnTimeOver()
		{
			_rigidbody2D.linearVelocity = Vector2.zero;
		}
	}
}