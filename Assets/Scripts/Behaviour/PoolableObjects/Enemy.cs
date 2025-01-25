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

			if (FeverManager.Instance.FeverTimeGageController.IsFeverTime is false)
			{
				OneCycleRecordStore.KilledEnemiesForFeverTime.Value++;
			}
			else
			{
				OneCycleRecordStore.CurrentFeverMultiplier.Value += ConstantStore.FeverMultiplierIncreaseStep;
				
				FeverManager.Instance.FeverTimeGageController.AddValue = 1.0f / ConstantStore.RequireKilledEnemyCountToSetFeverMode;
			}
			
			OneCycleRecordStore.KilledEnemies.Value++;
			
			OneCycleRecordStore.Score.Value += (int)(ConstantStore.DefaultKillScore * (OneCycleRecordStore.CurrentFeverMultiplier.Value  * 0.1f));

			_enemyPool.Release(this);
		}
	}
}