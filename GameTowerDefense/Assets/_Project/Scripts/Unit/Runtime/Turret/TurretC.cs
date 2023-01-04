using System.Collections.Generic;
using System.Linq;
using TowerDefense.Unit.Enemy.Runtime;
using TowerDefense.Unit.Runtime;
using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Turret.Runtime
{
    public sealed class TurretC : BaseTurret
    {
        private List<float> distanceEnemys = new List<float>();

        protected override BaseEnemy SetTarget()
        {
            distanceEnemys.Clear();
            
            if (tempTargets.Count == 0) return null;
            
            for (int i = 0; i < tempTargets.Count; i++)
            {
                distanceEnemys.Add(Vector3.Distance(this.transform.position,
                    tempTargets[i].gameObject.transform.position));
            }

            var min = distanceEnemys.Min();
            var indexOf = distanceEnemys.IndexOf(min);
            return tempTargets[indexOf];
        }

        protected override void SpawnBullet()
        {
            GameObject bullet = poolManager.GetPooledObject(PoolObjectType.TurretBulletTypeC);

            bullet.transform.position = spawnPointBullet.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<BaseBullet>()
                .Init(target, Damage, unitType, effectAttack, slowEnemy, explodeRange, layerEnemy);
            bullet.SetActive(true);
        }
    }
}