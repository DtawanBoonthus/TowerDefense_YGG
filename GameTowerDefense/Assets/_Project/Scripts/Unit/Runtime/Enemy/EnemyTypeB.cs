using TowerDefense.Unit.Runtime;

namespace TowerDefense.Unit.Enemy.Runtime
{
    public sealed class EnemyTypeB : BaseEnemy, IDamageable
    {
        public void TakeDamage(int damage, UnitTypes typeUnityAttack)
        {
            int finalDamage = damage;
            
            if (unitType == typeUnityAttack)
            {
                finalDamage += (int)(damage * status.DamageWeak) / 100;
            }
            
            if (currentHp - finalDamage <= 0)
            {
                currentHp = 0;
                Dead();
            }
            else
            {
                currentHp -= finalDamage;
            }
        }
        
        public void TakeEffect(Effects effect, float decreaseSpeed)
        {
            if (effect != Effects.Slow) return;
            Slow(decreaseSpeed);
        }
    }
}