using System.Collections;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime.State
{
    public sealed class GamePrepareState : BaseGameState
    {
        private IEnumerator coroutineTimePrepareState;
        
        public GamePrepareState(GameManager gameManager)
        {
            State = GameState.Prepare;
            GameManager = gameManager;
        }

        public override void EnterState()
        {
            GameManager.UIInfo.StateText.text = $"{State}";
            coroutineTimePrepareState = TimePrepareState();
            GameManager.GetCountIncreaseStatusEnemy();

            GameManager.StartCoroutine(coroutineTimePrepareState);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            GameManager.StopCoroutine(coroutineTimePrepareState);
        }

        private IEnumerator TimePrepareState()
        {
            yield return new WaitForSeconds(GameManager.GameManagerData.TimePrepareState);
            GameManager.PlayState();
        }
    }
}