namespace TowerDefense.Unit.Runtime
{
    public interface IDamageable
    {
        public void TakeDamage(int damage, UnitTypes typeUnityAttack);
        public void TakeEffect(Effects effect, float decreaseSpeed);
    }
}