using TowerDefense.Manager.GameManager.Runtime;
using TowerDefense.Unit.Enemy.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Runtime
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField] protected GameManagerSO gameManagerData;
        [SerializeField] protected Color color;
        [SerializeField] protected float speed;
        
        protected Renderer render;
        protected BaseEnemy target;
        protected int damage;
        protected UnitTypes unitType;
        protected Effects effect;
        protected float decreaseSpeed;
        protected float explodeRange;
        protected LayerMask layerEnemy;

        protected virtual void Awake()
        {
            render = GetComponentInChildren<Renderer>();
            render.material.color = color;
        }

        public virtual void Init(BaseEnemy target, int damage, UnitTypes unitType, Effects effect,
            float decreaseSpeed, float explodeRange, LayerMask layerEnemy)
        {
            this.target = target;
            this.damage = damage;
            this.unitType = unitType;
            this.effect = effect;
            this.decreaseSpeed = decreaseSpeed;
            this.explodeRange = explodeRange;
            this.layerEnemy = layerEnemy;
        }
        
        protected virtual void FixedUpdate()
        {
            if (target.IsDead)
            {
                this.gameObject.SetActive(false);
            }

            Movement();
        }

        protected virtual void Movement()
        {
            this.transform.Translate(
                (target.gameObject.transform.position - this.transform.position).normalized *
                (speed * Time.fixedDeltaTime), Space.World);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable enemy)) return;

            enemy.TakeDamage(damage, unitType);
            enemy.TakeEffect(effect, decreaseSpeed);

            this.gameObject.SetActive(false);
        }
    }
}