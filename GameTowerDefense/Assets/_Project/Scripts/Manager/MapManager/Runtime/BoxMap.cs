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
        internal bool IsCanSpawnWaypoint { get; set; } = false;
    }
}