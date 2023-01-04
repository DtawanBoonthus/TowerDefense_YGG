using System.Collections.Generic;
using TowerDefense.Utilities.PoolingPattern.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime.State
{
    public sealed class GameEndState : BaseGameState
    {
        private List<GameObject> enemiesA = new List<GameObject>();
        private List<GameObject> enemiesB = new List<GameObject>();
        private List<GameObject> enemiesC = new List<GameObject>();
        
        public GameEndState(GameManager gameManager)
        {
            State = GameState.End;
            GameManager = gameManager;
        }

        public override void EnterState()
        {
            GameManager.UIInfo.StateText.text = $"{State}";
            GameManager.UIInfo.RestartPanel.gameObject.SetActive(true);
            
            enemiesA = PooledObjects(PoolObjectType.EnemyUnitTypeA);
            enemiesB = PooledObjects(PoolObjectType.EnemyUnitTypeB);
            enemiesC = PooledObjects(PoolObjectType.EnemyUnitTypeC);

            var allEnemies = new[] { enemiesA, enemiesB, enemiesC };
            
            for (int i = allEnemies.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < allEnemies[i].Count; j++)
                {
                    allEnemies[i][j].SetActive(false);
                }
            }
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }
        
        private List<GameObject> PooledObjects(PoolObjectType objectType)
        {
            return GameManager.PoolManager.poolObjectSets
                .Find(x => x.PoolObjectType == objectType).PooledObjects;
        }
    }
}