using System.Collections;
using System.Collections.Generic;
using TowerDefense.Manager.GameManager.Runtime;
using TowerDefense.Unit.Enemy.Runtime;
using TowerDefense.Unit.Runtime;
using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Unit.Turret.Runtime
{
    public abstract class BaseTurret : MonoBehaviour
    {
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected TurretStatusSO status;
        [SerializeField] protected GameManagerSO gameManagerData;
        [SerializeField] protected LayerMask layerEnemy;
        [SerializeField] protected Transform spawnPointBullet;

        protected UnitTypes unitType;
        protected int minDamage;
        protected int maxDamage;
        protected float fireRate;
        protected float attackRange;
        protected Effects effectAttack;
        protected float slowEnemy;
        protected float explodeRange;
        protected float rotationSpeed;
        protected BaseEnemy target;
        protected List<BaseEnemy> tempTargets;

        internal int Damage => Random.Range(minDamage, maxDamage + 1);
        internal TurretStatusSO Status => status;

        protected virtual void OnEnable()
        {
            unitType = status.UnitType;
            minDamage = status.MinDamage;
            maxDamage = status.MaxDamage;
            fireRate = status.FireRate;
            attackRange = status.AttackRange;
            effectAttack = status.EffectAttack;
            slowEnemy = status.SlowEnemy;
            explodeRange = status.ExplodeRange;
            rotationSpeed = status.RotationSpeed;
            target = null;
            
            StartCoroutine(nameof(Fire));
        }

        protected abstract BaseEnemy SetTarget();

        protected virtual void FixedUpdate()
        {
            RadiusAttack();
            target = SetTarget();
            LookAtTarget();
        }

        protected virtual void LookAtTarget()
        {
            if (target == null) return;

            Quaternion rotation =
                Quaternion.LookRotation(new Vector3(target.gameObject.transform.position.x, 0,
                                            target.gameObject.transform.position.z) -
                                        new Vector3(transform.position.x, 0, transform.position.z), Vector3.up);

            this.transform.rotation =
                Quaternion.Lerp(this.transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        }

        protected virtual void RadiusAttack()
        {
            tempTargets = new List<BaseEnemy>();
            Collider[] hitColliders = new Collider[gameManagerData.EnemiesInRound.Count];
            int numColliders =
                Physics.OverlapSphereNonAlloc(this.transform.position, attackRange, hitColliders, layerEnemy);

            for (int i = 0; i < numColliders; i++)
            {
                if (!hitColliders[i].TryGetComponent(out BaseEnemy enemy)) continue;
                tempTargets.Add(enemy);
            }
        }
        
        protected virtual IEnumerator Fire()
        {
            while (true)
            {
                while (target == null)
                {
                    yield return null;
                }
                SpawnBullet();
                yield return new WaitForSeconds(fireRate);
            }
        }

        protected abstract void SpawnBullet();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);
        }
#endif
    }
}