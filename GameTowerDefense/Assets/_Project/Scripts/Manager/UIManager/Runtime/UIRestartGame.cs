using TowerDefense.Utilities.GameEvent.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Manager.UIManager.Runtime
{
    public sealed class UIRestartGame : MonoBehaviour
    {
        [SerializeField] private RectTransform restartPanel;
        [SerializeField] private Button restartButton; 
        [SerializeField] private GameEventSO restartGameEvent;

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            restartPanel.gameObject.SetActive(false);
            restartGameEvent.TriggerEvent();
        }
    }
}