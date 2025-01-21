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

			// EffectManager.Instance.ShowEffect("enemy_die", col.transform.position);
			// SoundManager.Instance.Play("Enemy Die");

			// GlobalDataContainer.score.Value += (int)(GlobalDataContainer.DefaultEnemyScore * GlobalDataContainer.feverMultiplier.Value);
			// GlobalDataContainer.killedEnemyCount.Value++;
			// RecordDataContainer.totalKilledEnemyCount++;

			_enemyPool.Release(this);
		}
	}
}