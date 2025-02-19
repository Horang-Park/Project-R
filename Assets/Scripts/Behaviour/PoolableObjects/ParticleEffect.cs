using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Behaviour.PoolableObjects
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleEffect : MonoBehaviour
	{
		private ParticleSystem _particleSystem;
		
		private Stack<GameObject> _effectPool;
		public Stack<GameObject> EffectPool
		{
			set => _effectPool = value;
		}

		private void Awake()
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}

		private void Start()
		{
			Observable.EveryUpdate()
				.Where(_ => _particleSystem.isStopped)
				.Subscribe(_ => AutoHide())
				.AddTo(this);
		}

		private void AutoHide()
		{
			gameObject.SetActive(false);
			transform.position = Vector3.zero;
			
			_effectPool.Push(gameObject);
		}
	}
}