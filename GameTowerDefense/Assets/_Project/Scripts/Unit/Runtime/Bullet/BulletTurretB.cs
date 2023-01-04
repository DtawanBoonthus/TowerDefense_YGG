using UnityEngine;

namespace TowerDefense.Unit.Runtime
{
    public sealed class BulletTurretB : BaseBullet
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable targetEnemy)) return;

            if (effect != Effects.Explode) return;
            
            Collider[] hitColliders = new Collider[gameManagerData.EnemiesInRound.Count];
            int numColliders =
                Physics.OverlapSphereNonAlloc(this.transform.position, explodeRange, hitColliders, layerEnemy);

            for (int i = 0; i < numColliders; i++)
            {
                if (!hitColliders[i].TryGetComponent(out IDamageable enemy)) continue;
                
                enemy.TakeDamage(damage, unitType);
                enemy.TakeEffect(effect, decreaseSpeed);
            }
            
            this.gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, explodeRange);
        }
#endif
    }
}