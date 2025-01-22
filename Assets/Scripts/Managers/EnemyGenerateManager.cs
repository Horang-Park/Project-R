using System;
using Behaviour.PoolableObjects;
using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Managers
{
	public class EnemyGenerateManager : MonoSingleton<EnemyGenerateManager>
	{
		[Header("Prefabs")]
		[SerializeField] private GameObject enemyPrefab;
		[Header("Generate preferences")]
		[SerializeField] private int initializeEnemyCount;
		[SerializeField] private Transform rangeTarget;
		[SerializeField] private float innerRange;
		[SerializeField] private float outerRange;

		private IObjectPool<Enemy> _enemyPool;
		private int _currentShowingEnemyCount;

		protected override void Awake()
		{
			_enemyPool = new ObjectPool<Enemy>(OnCreate, OnGet, OnRelease, OnDestruction);
		}

		private void Start()
		{
			for (var i = 0; i < initializeEnemyCount; i++)
			{
				_enemyPool.Get(out _);
			}

			Observable.EveryUpdate()
				.ThrottleFirst(TimeSpan.FromMilliseconds(500))
				.Where(_ => _currentShowingEnemyCount <= initializeEnemyCount * 0.75f)
				.Subscribe(_ => EnemyAutoGenerator())
				.AddTo(this);
		}

		private Enemy OnCreate()
		{
			var enemy = Instantiate(enemyPrefab, transform);
			var component = enemy.GetComponent(typeof(Enemy)) as Enemy;
			component!.EnemyPool = _enemyPool;

			return component;
		}

		private void OnGet(Enemy enemy)
		{
			enemy.transform.position = GetCircularRandomizePosition();
			enemy.gameObject.SetActive(true);

			_currentShowingEnemyCount++;
		}

		private void OnRelease(Enemy enemy)
		{
			enemy.gameObject.SetActive(false);
			enemy.transform.position = Vector3.zero;

			_currentShowingEnemyCount--;
		}

		private static void OnDestruction(Enemy enemy)
		{
			Destroy(enemy);
		}

		private void EnemyAutoGenerator()
		{
			_enemyPool.Get(out _);
		}

		private Vector2 GetRandomizePosition()
		{
			var randomizedPosition = Vector2.zero;
			var positioningFlag = Random.Range(0, 100);

			switch (positioningFlag)
			{
				case <= 40:
				{
					var xy = Random.Range(0, 2) == 1;

					if (xy)
					{
						randomizedPosition.x += Random.Range(innerRange, outerRange);
						randomizedPosition.y -= Random.Range(innerRange, outerRange);
					}
					else
					{
						randomizedPosition.x -= Random.Range(innerRange, outerRange);
						randomizedPosition.y += Random.Range(innerRange, outerRange);
					}

					break;
				}
				case > 40 and <= 80 :
				{
					var xy = Random.Range(0, 2) == 1;

					if (xy)
					{
						randomizedPosition.x += Random.Range(innerRange, outerRange);
						randomizedPosition.y += Random.Range(innerRange, outerRange);
					}
					else
					{
						randomizedPosition.x -= Random.Range(innerRange, outerRange);
						randomizedPosition.y -= Random.Range(innerRange, outerRange);
					}

					break;
				}
				case > 80 and <= 90:
				{
					var xy = Random.Range(0, 2) == 1;

					if (xy)
					{
						randomizedPosition.x += Random.Range(innerRange, outerRange);
						randomizedPosition.y = rangeTarget.position.y;
					}
					else
					{
						randomizedPosition.x = rangeTarget.position.x;
						randomizedPosition.y += Random.Range(innerRange, outerRange);
					}

					break;
				}
				default:
				{
					var xy = Random.Range(0, 2) == 1;

					if (xy)
					{
						randomizedPosition.x -= Random.Range(innerRange, outerRange);
						randomizedPosition.y = rangeTarget.position.y;
					}
					else
					{
						randomizedPosition.x = rangeTarget.position.x;
						randomizedPosition.y -= Random.Range(innerRange, outerRange);
					}

					break;
				}
			}

			return randomizedPosition;
		}

		private Vector2 GetCircularRandomizePosition()
		{
			return Random.insideUnitCircle * outerRange;
		}
	}
}