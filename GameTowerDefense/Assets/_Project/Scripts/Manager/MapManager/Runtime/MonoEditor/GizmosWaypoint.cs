using UnityEngine;

namespace TowerDefense.Manager.MapManager.MonoEditor
{
    internal sealed class GizmosWaypoint : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Color gizmosColor = Color.red;
        [SerializeField] private float radius = 1f;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
#endif
    }
}