using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Manager.MapManager.Runtime
{
    [CreateAssetMenu(fileName = "New MapData", menuName = "TowerDefense/Data/MapData")]
    public sealed class MapDataSO : ScriptableObject
    {
        [Tooltip("Enemies move clockwise? True:yes | False:no")] 
        [SerializeField] private bool isClockwise = false;
        
        /// <summary>
        /// Enemies move clockwise? True:yes | False:no
        /// </summary>
        public bool IsClockwise => isClockwise;
        
        /// <summary>
        /// Created BoxMap.
        /// </summary>
        public List<BoxMap> BoxMaps { get; set; } = new List<BoxMap>();

        /// <summary>
        /// The destination the enemy will walk.
        /// </summary>
        public GameObject[] WaypointEnemies { get; set; } = new GameObject[4];

        public Vector3 MapCenter => new Vector3((MapWidth / 2) - 0.5f, 0, (MapLength / 2) - 0.5f);
        
        /// <summary>
        /// Spawn point (index).
        /// </summary>
        public int SpawnPoint { get; internal set; }
        
        /// <summary>
        /// Waypoint spawn location.
        /// </summary>
        internal Transform[] WaypointPositions { get; set; } = new Transform[4];

        internal float MapWidth { get; set; }
        internal float MapLength { get; set; }

        public BoxMap BoxSelect { get; set; } = null;
    }
}