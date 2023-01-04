using System.Collections;
using System.Collections.Generic;
using TowerDefense.Manager.GameManager.Runtime.State;
using TowerDefense.Manager.MapManager.Runtime;
using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;
using Random = System.Random;

namespace TowerDefense.Manager.GameManager.Runtime
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gameManagerData;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private MapDataSO mapData;
        [SerializeField] private EnemySettingInfo enemyInfo;
        [SerializeField] private UIInfo uiInfo;

        private Random random = new Random();
        private BaseGameState gameState;
        private GamePrepareState gamePrepareState;
        private GamePlayState gamePlayState;
        private GameEndState gameEndState;

        private readonly PoolObjectType[] poolEnemyTypes = new[]
            { PoolObjectType.EnemyUnitTypeA, PoolObjectType.EnemyUnitTypeB, PoolObjectType.EnemyUnitTypeC };

        internal GameManagerSO GameManagerData => gameManagerData;
        internal PoolManagerSO PoolManager => poolManager;
        
        internal EnemySettingInfo EnemyInfo => enemyInfo;
        internal UIInfo UIInfo => uiInfo;

        private void Awake()
        {
            InitiateUI();
            
            gameManagerData.Player.OnDead += PlayerOnDead;
            
            SetState();
            gameManagerData.TempCountIncreaseStatus = 0;
            poolManager.CreatePoolObj();

            SetSpawnPoint();
            
            StartCoroutine(nameof(EnemyUpgradeTime));
        }

        private void InitiateUI()
        {
            uiInfo.StartPanel.gameObject.SetActive(true);
            uiInfo.RestartPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            gameManagerData.Player.OnDead -= PlayerOnDead;
        }

        private void PlayerOnDead()
        {
            EndState();
        }

        private void SetState()
        {
            gamePrepareState = new GamePrepareState(this);
            gamePlayState = new GamePlayState(this);
            gameEndState = new GameEndState(this);
        }

        private void Update()
        {
           gameState?.UpdateState();
        }

        private void SetSpawnPoint()
        {
            List<BoxMap> boxWaypoints = mapData.BoxMaps.FindAll(x => x.IsCanSpawnWaypoint); // Find a box where a waypoint can be placed.
            (boxWaypoints[2], boxWaypoints[3]) = (boxWaypoints[3], boxWaypoints[2]); // Swap index 2,3.
            BoxMap boxSpawn = boxWaypoints[mapData.SpawnPoint];
            boxSpawn.gameObject.AddComponent<SpawnTrigger>();
        }

        internal IEnumerator SpawnEnemy()
        {
            InitiateSpawnEnemy();
            
            for (int i = 0; i < gameManagerData.EnemiesInRound.Count; i++)
            {
                GameObject enemy = poolManager.GetPooledObject(gameManagerData.EnemiesInRound[i].PoolType);

                enemy.transform.position = mapData.WaypointEnemies[mapData.SpawnPoint].transform.position +
                                           enemyInfo.OffsetSpawnEnemy;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
                yield return new WaitForSeconds(enemyInfo.DelaySpawn);
            }
        }

        private void InitiateSpawnEnemy()
        {
            gameManagerData.EnemiesInRound.Clear();
            
            for (int i = 0; i < enemyInfo.CountEnemy; i++)
            {
                EnemiesInRound enemyType = new EnemiesInRound
                {
                    PoolType = poolEnemyTypes[random.Next(poolEnemyTypes.Length)],
                    IsHide = false
                };
                gameManagerData.EnemiesInRound.Add(enemyType);
            }
        }

        private IEnumerator EnemyUpgradeTime()
        {
            while (true)
            {
                gameManagerData.TimeEnemyUpgrade = enemyInfo.TimeIncreaseStatus;
                
                while (gameManagerData.TimeEnemyUpgrade > 0)
                {
                    if (gameState?.State == GameState.Play)
                    {
                        gameManagerData.TimeEnemyUpgrade -= Time.deltaTime;
                        float seconds = Mathf.FloorToInt(gameManagerData.TimeEnemyUpgrade % 60);
                        uiInfo.TimerText.text = $"{seconds:0}";
                    }

                    yield return null;
                }
                
                gameManagerData.TempCountIncreaseStatus++;
                uiInfo.NextLevelText.text = gameManagerData.TempCountIncreaseStatus.ToString();
            }
        }
        
        public void PlayState()
        {
            ChangeStateTo(gamePlayState);
        }
        
        public void PrepareState()
        {
            ChangeStateTo(gamePrepareState);
        }

        public void EndState()
        {
            ChangeStateTo(gameEndState);
        }

        public void RestartGame()
        {
            gameManagerData.Player.ResetHp();
            gameManagerData.TempCountIncreaseStatus = 0;
            PrepareState();
        }

        internal void GetCountIncreaseStatusEnemy()
        {
            for (int i = 0; i < enemyInfo.EnemyStatus.Length; i++)
            {
                enemyInfo.EnemyStatus[i].CountIncreaseStatus = gameManagerData.TempCountIncreaseStatus;
            }

            uiInfo.CurrentLevelText.text = gameManagerData.TempCountIncreaseStatus.ToString();
        }
        
        private void ChangeStateTo(BaseGameState nextState)
        {
            gameState?.ExitState();
            gameState = nextState;
            gameState?.EnterState();
        }
    }
}