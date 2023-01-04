using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Manager.MapManager.Runtime
{
    [DefaultExecutionOrder(-90)]
    internal sealed class GenerateMap : MonoBehaviour
    {
        #region VALUES
        
        [Tooltip("Map data ScriptableObject.")]
        [SerializeField] private MapDataSO mapData;
        
        [Space]
        [Tooltip("Prefab building area for generate.")]
        [SerializeField] private BoxMap boxBuildingAreaPrefab;
        [Tooltip("Prefab walking area for generate.")]
        [SerializeField] private BoxMap boxWalkingAreaPrefab;
        
        [Space]
        [Tooltip("Starting position generate map.")]
        [SerializeField] private Transform startPosition;
        [Tooltip("Parent building area.")]
        [SerializeField] private Transform parentBuildingArea;
        [Tooltip("Parent walking area.")]
        [SerializeField] private Transform parentWalkingArea;

        [Space] 
        [Tooltip("Start layer.")]
        [SerializeField] private string startLayer;
        [Tooltip("Map width.")]
        [SerializeField] [Min(3)] private int width;
        [Tooltip("Map length.")]
        [SerializeField] [Min(3)] private int length;

        [Space] 
        [Tooltip("Enemy spawn point (index).")] 
        [SerializeField] [Range(0, 3)] private int spawnPoint;

        [Space] 
        [Tooltip("Color box spawn position.")] 
        [SerializeField] private Color colorBoxSpawn;

        private BoxMap tempBoxMap;

        #endregion
        
        private void Awake()
        {
#if UNITY_EDITOR
            DebugAssert();
#endif
            mapData.SpawnPoint = spawnPoint;
            mapData.MapWidth = width;
            mapData.MapLength = length;
            
            Generate();
            SetBoxSpawnPosition();
        }

        /// <summary>
        /// Generate map.
        /// </summary>
        private void Generate()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    // Set box position.
                    Vector3 tempPosition = new Vector3(i + startPosition.position.x, 0, j + startPosition.position.z);
                    
                    if (i == 0 || i == width -1) // Generate box walking area at first row and last row.
                    {
                        InstantiateBoxWalkingArea(tempPosition);
                    }
                    else
                    {
                        if (j == 0 || j == length - 1) // Generate box walking area at first column and last column.
                        {
                            InstantiateBoxWalkingArea(tempPosition);
                        }
                        else // Generate box building area.
                        {
                            InstantiateBoxBuildingArea(tempPosition);
                        }
                    }
                    
                    tempBoxMap.HaveTower = false;
                    
                    // Set waypoint for enemy.
                    tempBoxMap.IsCanSpawnWaypoint = i == 0 && (j == 0 || j == length - 1) ||
                                                    i == width - 1 && (j == 0 || j == length - 1);

                    mapData.BoxMaps.Add(tempBoxMap);
                }
            }
        }
        
        /// <summary>
        /// Setting box spawn.
        /// </summary>
        private void SetBoxSpawnPosition()
        {
            List<BoxMap> boxWaypoints = mapData.BoxMaps.FindAll(x => x.IsCanSpawnWaypoint); // Find a box where a waypoint can be placed.
            (boxWaypoints[2], boxWaypoints[3]) = (boxWaypoints[3], boxWaypoints[2]); // Swap index 2,3.
            BoxMap boxSpawn = boxWaypoints[spawnPoint]; // Get spawn point.
            boxSpawn.gameObject.GetComponentInChildren<Renderer>().material.color = colorBoxSpawn; // Change color.
            boxSpawn.gameObject.AddComponent<BoxCollider>().isTrigger = true; // Add BoxCollider.
            boxSpawn.gameObject.layer = LayerMask.NameToLayer(startLayer); // Set layer.
            boxSpawn.gameObject.AddComponent<Rigidbody>().isKinematic = true; // Add Rigidbody.

            GetWaypointPosition(boxWaypoints);
        }

        /// <summary>
        /// Add waypoint position to data.
        /// </summary>
        /// <param name="boxWaypoints"> List BoxMap waypoint. </param>
        private void GetWaypointPosition(List<BoxMap> boxWaypoints)
        {
            for (int i = 0; i < mapData.WaypointPositions.Length; i++)
            {
                mapData.WaypointPositions[i] = boxWaypoints[i].gameObject.transform;
            }
        }

        /// <summary>
        /// Build box walking area.
        /// </summary>
        /// <param name="position"> Build at position? </param>
        private void InstantiateBoxWalkingArea(Vector3 position)
        {
           tempBoxMap = Instantiate(boxWalkingAreaPrefab, position, Quaternion.identity, parentWalkingArea);
           tempBoxMap.IsBoxBuilding = false;
        }
        
        /// <summary>
        /// Build box building area.
        /// </summary>
        /// <param name="position"> Build at position? </param>
        private void InstantiateBoxBuildingArea(Vector3 position)
        {
            tempBoxMap = Instantiate(boxBuildingAreaPrefab, position, Quaternion.identity, parentBuildingArea);
            tempBoxMap.IsBoxBuilding = true;
        }
        
        #region DEBUG

#if UNITY_EDITOR
        private void DebugAssert()
        {
            Debug.Assert(mapData != null, "mapData cannot be null");
            Debug.Assert(boxBuildingAreaPrefab != null, "boxBuildingAreaPrefab cannot be null");
            Debug.Assert(boxWalkingAreaPrefab != null, "boxWalkingAreaPrefab cannot be null");
            Debug.Assert(startPosition != null, "startPosition cannot be null");
            Debug.Assert(parentBuildingArea != null, "parentBuildingArea cannot be null");
            Debug.Assert(parentWalkingArea != null, "parentWalkingArea cannot be null");
        }
#endif

        #endregion
    }
}