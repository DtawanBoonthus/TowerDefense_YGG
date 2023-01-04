using UnityEngine;

namespace TowerDefense.Manager.MapManager.Runtime
{
    public sealed class BoxMap : MonoBehaviour
    {
        /// <summary>
        /// Is it a box building? True:yes | False:no
        /// </summary>
        public bool IsBoxBuilding { get; set; } = false;
        
        /// <summary>
        /// Can a waypoint be created on the box? True:yes | False:no
        /// </summary>
        public bool IsCanSpawnWaypoint { get; internal set; } = false;

        public bool HaveTower { get; set; }
    }
}