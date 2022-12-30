using UnityEngine;

namespace TowerDefense.Manager.MapManager.Runtime
{
    [DefaultExecutionOrder(-89)]
    internal sealed class GenerateWaypoint : MonoBehaviour
    {
        #region VALUES

        [Tooltip("Map data ScriptableObject.")] 
        [SerializeField] private MapDataSO mapData;

        [Space]
        [Tooltip("Prefab way point for generate.")] 
        [SerializeField] private GameObject waypointPrefab;
        [Tooltip("Parent waypoint.")]
        [SerializeField] private Transform parentWaypoint;

        #endregion
        
        private void Awake()
        {
#if UNITY_EDITOR
            DebugAssert();
#endif
            Generate();
        }

        /// <summary>
        /// Generate Waypoint.
        /// </summary>
        private void Generate()
        {
            for (var i = 0; i < mapData.WaypointPositions.Length; i++)
            {
                GameObject waypoint = Instantiate(waypointPrefab, mapData.WaypointPositions[i].position,
                    Quaternion.identity, parentWaypoint);
                mapData.WaypointEnemies[i] = waypoint;
            }
        }
        
#if UNITY_EDITOR
        private void DebugAssert()
        {
            Debug.Assert(mapData != null, "mapData cannot be null");
            Debug.Assert(waypointPrefab != null, "waypointPrefab cannot be null");
            Debug.Assert(parentWaypoint != null, "parentWaypoint cannot be null");
        }
#endif
    }
}