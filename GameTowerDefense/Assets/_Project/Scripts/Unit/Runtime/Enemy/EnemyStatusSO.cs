using TowerDefense.Unit.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Enemy.Runtime
{
    [CreateAssetMenu(fileName = "New EnemyStatus", menuName = "TowerDefense/Data/EnemyStatus")]
    public sealed class EnemyStatusSO : ScriptableObject
    {
        [SerializeField] private float maxHp;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        [SerializeField] private UnitTypes unitType;
        [SerializeField] [Min(1)] private int countTrigger = 1;
        [SerializeField] private float damageWeak;
        [Tooltip("Increase status (Percent %).")]
        [SerializeField] private float increaseStatus;
        
        public float MaxHp => maxHp;
        public float Speed => speed;
        public int Damage => damage;
        public UnitTypes UnitType => unitType;
        public int CountTrigger => countTrigger;
        public float DamageWeak => damageWeak;
        public float IncreaseStatus => increaseStatus;
        public int CountIncreaseStatus { get; set; }

        private void OnEnable()
        {
            CountIncreaseStatus = 0;
        }
    }
}