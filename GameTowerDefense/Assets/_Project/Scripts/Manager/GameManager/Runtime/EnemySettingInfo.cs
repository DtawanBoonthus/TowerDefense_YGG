using System;
using TowerDefense.Unit.Enemy.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime
{
    [Serializable]
    internal struct EnemySettingInfo
    {
        [SerializeField] private EnemyStatusSO[] enemyStatus;
        [SerializeField] private Vector3 offsetSpawnEnemy;
        [SerializeField] private int countEnemy;
        [SerializeField] private float delaySpawn;
        [SerializeField] private float timeIncreaseStatus;

        internal EnemyStatusSO[] EnemyStatus => enemyStatus;
        internal Vector3 OffsetSpawnEnemy => offsetSpawnEnemy;
        internal int CountEnemy => countEnemy;
        internal float DelaySpawn => delaySpawn;
        internal float TimeIncreaseStatus => timeIncreaseStatus;
    }
}