using TowerDefense.Utilities.GameEvent.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Manager.UIManager.Runtime
{
    public sealed class UIStartGame : MonoBehaviour
    {
        [SerializeField] private RectTransform startPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private GameEventSO startGameEvent;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            startPanel.gameObject.SetActive(false);
            startGameEvent.TriggerEvent();
        }
    }
}