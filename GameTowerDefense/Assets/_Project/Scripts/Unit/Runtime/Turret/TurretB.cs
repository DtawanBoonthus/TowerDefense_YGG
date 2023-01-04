using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.Unit.Enemy.Runtime;
using TowerDefense.Unit.Runtime;
using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Turret.Runtime
{
    public sealed class TurretB : BaseTurret
    {
        private List<float> distanceEnemys = new List<float>();

        protected override BaseEnemy SetTarget()
        {
            distanceEnemys.Clear();

            if (tempTargets.Count == 0) return null;
            
            var enemyMaxHp = tempTargets.Max(x => x.CurrentHp);
            var enemiesMaxHp = tempTargets.FindAll(x => Math.Abs(x.CurrentHp - enemyMaxHp) == 0);
                
            if (enemiesMaxHp.Count > 1)
            {
                for (int i = 0; i < enemiesMaxHp.Count; i++)
                {
                    distanceEnemys.Add(Vector3.Distance(this.transform.position,
                        enemiesMaxHp[i].gameObject.transform.position));
                }
            }
            else
            {
                return enemiesMaxHp[Index.Start];
            }

            var maxDistance = distanceEnemys.Max();
            var indexDistanceOf = distanceEnemys.IndexOf(maxDistance);

            return enemiesMaxHp[indexDistanceOf];
        }

        protected override void SpawnBullet()
        {
            GameObject bullet = poolManager.GetPooledObject(PoolObjectType.TurretBulletTypeB);

            bullet.transform.position = spawnPointBullet.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<BaseBullet>()
                .Init(target, Damage, unitType, effectAttack, slowEnemy, explodeRange, layerEnemy);
            bullet.SetActive(true);
        }
    }
}