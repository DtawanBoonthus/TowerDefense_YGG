using TowerDefense.Unit.Runtime;
using TowerDefense.Utilities.PoolingPattern.Runtime;

namespace TowerDefense.Manager.GameManager.Runtime
{
    public class EnemiesInRound
    {
        public PoolObjectType PoolType { get; set; }

        public UnitTypes UnitType
        {
            get
            {
                return PoolType switch
                {
                    PoolObjectType.EnemyUnitTypeA => UnitTypes.TypeA,
                    PoolObjectType.EnemyUnitTypeB => UnitTypes.TypeB,
                    PoolObjectType.EnemyUnitTypeC => UnitTypes.TypeC,
                    _ => UnitTypes.None
                };
            }
        }

        public bool IsHide { get; set; }
    }
}