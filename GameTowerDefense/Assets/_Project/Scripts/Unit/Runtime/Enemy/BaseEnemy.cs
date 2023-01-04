using TowerDefense.Manager.GameManager.Runtime;
using TowerDefense.Manager.MapManager.Runtime;
using TowerDefense.Unit.Runtime;
using UnityEngine;

namespace TowerDefense.Unit.Enemy.Runtime
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected GameManagerSO gameManagerData;
        [SerializeField] protected MapDataSO mapData;
        [SerializeField] protected EnemyStatusSO status;
        
        protected float maxHp;
        protected float currentHp;
        protected float speed;
        protected UnitTypes unitType;
        protected float damageWeak;

        private int targetWaypoint = 0;
        private Vector3 directionMove =>
            mapData.WaypointEnemies[targetWaypoint].transform.position -
            new Vector3(this.transform.position.x, 0, this.transform.position.z);

        public bool IsDead { get; private set; }
        public int TriggerEndPoint { get; set; } = 0;
        public float CurrentHp => currentHp;
        public EnemyStatusSO Status => status;
        
        protected virtual void OnEnable()
        {
            maxHp = status.MaxHp;
            speed = status.Speed;
            unitType = status.UnitType;
            targetWaypoint = mapData.SpawnPoint;
            TriggerEndPoint = 0;
            damageWeak = status.DamageWeak;
            IsDead = false;
            
            IncreaseStatus();
            
            currentHp = maxHp;
            Debug.Log($"Status enemy {unitType} Count {status.CountIncreaseStatus}: Hp {maxHp} : Speed {speed}");
        }

        protected virtual void OnDisable()
        {
            IsDead = true;
        }

        protected virtual void FixedUpdate()
        {
            Movement();
        }

        protected virtual void Movement()
        {
            this.transform.Translate(directionMove.normalized * (speed * Time.fixedDeltaTime), Space.World);
            
            if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), 
                    new Vector3(mapData.WaypointEnemies[targetWaypoint].transform.position.x, 0,
                        mapData.WaypointEnemies[targetWaypoint].transform.position.z)) <= 0.1f)
            {
                NextWaypoint();
            }
            
            Quaternion rotation =
                Quaternion.LookRotation(new Vector3(mapData.WaypointEnemies[targetWaypoint].transform.position.x, 0,
                    mapData.WaypointEnemies[targetWaypoint].transform.position.z) - new Vector3(transform.position.x, 0,
                    transform.position.z), Vector3.up);
            this.transform.rotation = rotation;
        }

        protected virtual void IncreaseStatus()
        {
            for (int i = 0; i < status.CountIncreaseStatus; i++)
            {
                maxHp += (maxHp * status.IncreaseStatus) / 100;
                speed += (speed * status.IncreaseStatus) / 100;
            }
        }

        protected virtual void Dead()
        {
            SetEnemyInRound();
            this.gameObject.SetActive(false);
        }
        
        protected virtual void Slow(float decreaseSpeed)
        {
            speed -= (speed * decreaseSpeed) / 100;
        }

        public virtual void GotoEndSpawn()
        {
            SetEnemyInRound();
            gameManagerData.Player.GetDamage(status.Damage);
        }

        private void SetEnemyInRound()
        {
            var enemyInRound = gameManagerData.EnemiesInRound.Find(
                x => x.UnitType == unitType && x.IsHide == false);
            enemyInRound.IsHide = true;
        }
        
        private void NextWaypoint()
        {
            if (mapData.IsClockwise)
            {
                if (targetWaypoint < mapData.WaypointEnemies.Length - 1)
                {
                    targetWaypoint++;
                }
                else
                {
                    targetWaypoint = 0;
                }
            }
            else
            {
                if (targetWaypoint != 0)
                {
                    targetWaypoint--;
                }
                else
                {
                    targetWaypoint = mapData.WaypointEnemies.Length - 1;
                }
            }
        }
    }
}