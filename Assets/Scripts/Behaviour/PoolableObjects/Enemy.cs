using Horang.HorangUnityLibrary.Managers.Module;
using Horang.HorangUnityLibrary.Modules.AudioModule;
using Managers;
using Stores;
using UnityEngine;
using UnityEngine.Pool;

namespace Behaviour.PoolableObjects
{
	public class Enemy : MonoBehaviour
	{
		private IObjectPool<Enemy> _enemyPool;

		public IObjectPool<Enemy> EnemyPool
		{
			set => _enemyPool = value;
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.collider.CompareTag("Player") is false)
			{
				return;
			}

			ParticleEffectManager.Instance.ShowEffect("Enemy Die", col.transform.position);
			// ModuleManager.Instance.GetModule<AudioModule>()!.Play("Enemy Die");

			OneCycleRecordStore.KilledEnemies.Value++;
			OneCycleRecordStore.Score.Value += (int)(ConstantStore.DefaultKillScore * OneCycleRecordStore.CurrentFeverMultiplier.Value);
			
			// RecordDataContainer.totalKilledEnemyCount++;

			_enemyPool.Release(this);
		}
	}
}