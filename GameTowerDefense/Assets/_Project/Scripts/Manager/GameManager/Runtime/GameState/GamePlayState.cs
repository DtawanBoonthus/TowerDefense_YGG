using System.Collections;

namespace TowerDefense.Manager.GameManager.Runtime.State
{
    public sealed class GamePlayState : BaseGameState
    {
        private IEnumerator coroutineSpawnEnemy;
        
        public GamePlayState(GameManager gameManager)
        {
            State = GameState.Play;
            GameManager = gameManager;
        }

        public override void EnterState()
        {
            GameManager.UIInfo.StateText.text = $"{State}";
            coroutineSpawnEnemy = GameManager.SpawnEnemy();
            
            GameManager.StartCoroutine(coroutineSpawnEnemy);
        }

        public override void UpdateState()
        {
            var enemies = GameManager.GameManagerData.EnemiesInRound.FindAll(x => x.IsHide == true);
            
            if (enemies.Count == GameManager.GameManagerData.EnemiesInRound.Count)
            {
                GameManager.PrepareState();
            }
        }
        
        public override void ExitState()
        {
            GameManager.StopCoroutine(coroutineSpawnEnemy);
        }
    }
}