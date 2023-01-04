using System;
using TMPro;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime
{
    [Serializable]
    internal struct UIInfo
    {
        [SerializeField] private RectTransform startPanel;
        [SerializeField] private RectTransform restartPanel;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;
        [SerializeField] private TextMeshProUGUI stateText;
        
        internal RectTransform StartPanel => startPanel;
        internal RectTransform RestartPanel => restartPanel;
        internal TextMeshProUGUI TimerText => timerText;
        internal TextMeshProUGUI CurrentLevelText => currentLevelText;
        internal TextMeshProUGUI NextLevelText => nextLevelText;
        internal TextMeshProUGUI StateText => stateText;
    }
}