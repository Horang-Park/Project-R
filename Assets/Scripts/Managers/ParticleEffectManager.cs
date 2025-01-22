using System;
using System.Collections.Generic;
using Behaviour.PoolableObjects;
using Horang.HorangUnityLibrary.Foundation;
using UnityEngine;

namespace Managers
{
	[Serializable]
	public struct EffectData
	{
		public string name;
		public GameObject prefab;
		public int initializeEffectCount;
	}
	
	public class ParticleEffectManager : MonoSingleton<ParticleEffectManager>
	{
		public List<EffectData> effects;

		private readonly Dictionary<int, Stack<GameObject>> _effectsDictionary = new();
		
		protected override void Awake()
		{
			foreach (var effectItem in effects)
			{
				var key = effectItem.name.GetHashCode();

				if (_effectsDictionary.ContainsKey(key))
				{
					continue;
				}
				
				_effectsDictionary.Add(key, new Stack<GameObject>());

				for (var i = 0; i < effectItem.initializeEffectCount; i++)
				{
					var effect = Instantiate(effectItem.prefab, transform);
					var component = effect.GetComponent<ParticleEffect>();
					component!.EffectPool = _effectsDictionary[key];
					
					effect.gameObject.SetActive(false);
						
					_effectsDictionary[key].Push(effect);
				}
			}
		}

		public void ShowEffect(string effectName, Vector3 targetPosition)
		{
			var key = effectName.GetHashCode();
			var effectItem = _effectsDictionary[key].Pop();

			effectItem.transform.position = targetPosition;
			effectItem.gameObject.SetActive(true);
		}
	}
}