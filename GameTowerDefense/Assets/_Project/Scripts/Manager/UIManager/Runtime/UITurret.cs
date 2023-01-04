using TowerDefense.Utilities.GameEvent.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Manager.UIManager.Runtime
{
    public sealed class UITurret : MonoBehaviour
    {
        [SerializeField] private Button turretA;
        [SerializeField] private Button turretB;
        [SerializeField] private Button turretC;
        
        [SerializeField] private GameEventSO turretAGameEvent;
        [SerializeField] private GameEventSO turretBGameEvent;
        [SerializeField] private GameEventSO turretCGameEvent;

        private void Awake()
        {
            turretA.onClick.AddListener(turretAGameEvent.TriggerEvent);
            turretB.onClick.AddListener(turretBGameEvent.TriggerEvent);
            turretC.onClick.AddListener(turretCGameEvent.TriggerEvent);
        }

        private void OnDisable()
        {
            turretA.onClick.RemoveAllListeners();
            turretB.onClick.RemoveAllListeners();
            turretC.onClick.RemoveAllListeners();
        }
    }
}