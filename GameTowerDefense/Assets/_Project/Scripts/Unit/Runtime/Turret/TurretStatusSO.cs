using TowerDefense.Unit.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Turret.Runtime
{
    [CreateAssetMenu(fileName = "New TurretStatus", menuName = "TowerDefense/Data/TurretStatus")]
    public sealed class TurretStatusSO : ScriptableObject
    {
        [SerializeField] private UnitTypes unitType;
        [SerializeField] private int minDamage;
        [SerializeField] private int maxDamage;
        [SerializeField] private float fireRate;
        [SerializeField] private float attackRange;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Effects effectAttack;
        [SerializeField] [Range(0, 100)] private float slowEnemy;
        [SerializeField] private float explodeRange;

        public UnitTypes UnitType => unitType;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public float FireRate => fireRate;
        public float AttackRange => attackRange;
        public float RotationSpeed => rotationSpeed;
        public Effects EffectAttack => effectAttack;
        public float SlowEnemy => slowEnemy;
        public float ExplodeRange => explodeRange;
    }
}