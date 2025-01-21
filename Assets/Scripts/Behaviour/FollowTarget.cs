using UniRx;
using UnityEngine;

namespace Behaviour
{
	public class FollowToTarget : MonoBehaviour
	{
		[SerializeField] private Transform makeToFollow;
		[SerializeField] private Transform target;

		[Header("Preferences")]
		[SerializeField] private Vector3 offset;
		[SerializeField] [Range(0.1f, 10.0f)] private float speed = 5.0f;
		[SerializeField] private float snappingRange = 0.001f;

		public FloatReactiveProperty remainDistanceNonSquare = new();

		public Transform Target
		{
			set => target = value;
		}

		private void Start()
		{
			Observable.EveryFixedUpdate()
				.Where(_ => gameObject.activeSelf)
				.Select(_ => Time.deltaTime)
				.Subscribe(Follow)
				.AddTo(this);
		}

		private void Follow(float deltaTime)
		{
			if (target is null || !target)
			{
				return;
			}
		
			var desiredPosition = target.position + offset;
			var smoothedPosition = Vector3.Lerp(makeToFollow.position, desiredPosition, speed * deltaTime);
			makeToFollow.position = smoothedPosition;

			var distance = (desiredPosition - makeToFollow.position).sqrMagnitude;

			remainDistanceNonSquare.Value = distance;

			if (distance <= snappingRange)
			{
				makeToFollow.position = desiredPosition;
			}
		}
	}
}