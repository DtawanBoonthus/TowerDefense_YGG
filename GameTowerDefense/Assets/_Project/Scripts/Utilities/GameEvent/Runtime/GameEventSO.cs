using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Utilities.GameEvent.Runtime
{
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "TowerDefense/Utilities/Game Event")]
    public sealed class GameEventSO : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void TriggerEvent()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventTriggered();
            }
        }

        public void AddListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}