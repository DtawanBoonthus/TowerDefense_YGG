using System.Collections.Generic;
using TowerDefense.Player.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime
{
    [CreateAssetMenu(fileName = "New GameManager", menuName = "TowerDefense/Data/GameManagerData")]
    public sealed class GameManagerSO : ScriptableObject
    {
        [SerializeField] private float timePrepareState;

        public PlayerSO Player;
        public List<EnemiesInRound> EnemiesInRound { get; set; } = new List<EnemiesInRound>();
        public float TimePrepareState => timePrepareState;
        public float TimeEnemyUpgrade { get; set; }
        public int TempCountIncreaseStatus { get; set; }
    }
}