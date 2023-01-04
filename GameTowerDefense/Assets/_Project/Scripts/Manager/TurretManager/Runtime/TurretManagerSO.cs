using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.TowerManager.Runtime
{
    [CreateAssetMenu(fileName = "New TurretManager", menuName = "TowerDefense/Data/TurretManager")]
    public sealed class TurretManagerSO : ScriptableObject
    {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private Vector3 offset;

        public PoolObjectType CurrentTurret { get; private set; } = PoolObjectType.TurretUnitTypeA;
        
        public void BuildTurret(PoolObjectType turretType, Transform spawnPoint, out bool haveTurret, out GameObject turret)
        {
            GameObject poolTurret = poolManager.GetPooledObject(turretType);

            poolTurret.transform.position = spawnPoint.position + offset;
            poolTurret.transform.rotation = Quaternion.identity;
            poolTurret.SetActive(true);
            haveTurret = true;
            turret = poolTurret;
        }
        
        public void RemoveTurret(GameObject turret, out bool haveTurret)
        {
            turret.SetActive(false);
            haveTurret = false;
        }

        public void TurretA()
        {
            CurrentTurret = PoolObjectType.TurretUnitTypeA;
        }
        
        public void TurretB()
        {
            CurrentTurret = PoolObjectType.TurretUnitTypeB;
        }
        
        public void TurretC()
        {
            CurrentTurret = PoolObjectType.TurretUnitTypeC;
        }
    }
}