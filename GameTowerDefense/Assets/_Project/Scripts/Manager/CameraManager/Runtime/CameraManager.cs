using TowerDefense.Manager.MapManager.Runtime;
using UnityEngine;

namespace TowerDefense.Manager.CameraManager.Runtime
{
    [DefaultExecutionOrder(-88)]
    internal sealed class CameraManager : MonoBehaviour
    {
        #region VALUES
        
        [Tooltip("Map data ScriptableObject.")]
        [SerializeField] private MapDataSO mapData;

        [Space]
        [Tooltip("Camera look at target.")] 
        [SerializeField] private Transform targetViewpoint;

        #endregion

        private void Awake()
        {
#if UNITY_EDITOR
            DebugAssert();
#endif
           SetTargetViewpointPosition();
        }

        /// <summary>
        /// Setting target viewpoint position.
        /// </summary>
        private void SetTargetViewpointPosition()
        {
            targetViewpoint.position = mapData.MapCenter;
        }

        #region DEBUG

#if UNITY_EDITOR
        private void DebugAssert()
        {
            Debug.Assert(mapData != null, "mapData cannot be null");
            Debug.Assert(targetViewpoint != null, "targetViewpoint cannot be null");
        }
#endif

        #endregion
    }
}