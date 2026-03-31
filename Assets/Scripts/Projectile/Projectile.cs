using Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace Projectile
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private Rigidbody rB;
		[SerializeField] private float firingSpeed;
		[HideInInspector] public float damage;
		[HideInInspector] public float burnDamage;
		[HideInInspector] public float burnDuration;
		
		private IObjectPool<Projectile> _pool;

		private bool _isReturned;
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		public void LaunchProjectile()
		{
			rB.linearVelocity = transform.forward *  firingSpeed;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetPool(IObjectPool<Projectile> pool)
		{
			_pool = pool;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void ResetProjectile()
		{
			damage = 0f;
			burnDamage = 0f;
			burnDuration = 0f;
			_isReturned = false;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void ReturnToPool()
		{
			if (_isReturned) return;
			_isReturned = true;
			_pool?.Release(this);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) return;
			enemyHealth.TakeDamage(damage);
		
			//pls Burn you nasty Bitch
			if(burnDuration > 0)
			{
				other.GetComponent<Enemy.Enemy>().SetOnFire(burnDamage, burnDuration);
			}
			ReturnToPool();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnBecameInvisible()
		{
			ReturnToPool();
		}
	}
}
