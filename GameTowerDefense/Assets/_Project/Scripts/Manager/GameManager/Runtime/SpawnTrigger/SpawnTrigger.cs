using TowerDefense.Unit.Enemy.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.GameManager.Runtime
{
    public sealed class SpawnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out BaseEnemy target)) return;
            
            target.TriggerEndPoint++;
            
            if (target.TriggerEndPoint == target.Status.CountTrigger + 1)
            {
                target.GotoEndSpawn();
                target.gameObject.SetActive(false);
            }
        }
    }
}