using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Utilities.PoolingPattern.Runtime
{
    [CreateAssetMenu(fileName = "New PoolManager", menuName = "TowerDefense/Utilities/Pool Manager")]
    public sealed class PoolManagerSO : ScriptableObject
    {
        public List<PoolObjectSet> poolObjectSets;
        
        private Dictionary<PoolObjectType, PoolObjectSet> poolObjectSetDictionary;
        
        public void CreatePoolObj()
        {
            poolObjectSetDictionary = new Dictionary<PoolObjectType, PoolObjectSet>();
            foreach (PoolObjectSet poolObjectSet in poolObjectSets)
            {
                poolObjectSet.PooledObjects = new List<GameObject>();
                for (int i = 0; i < poolObjectSet.AmountToPool; i++)
                {
                    var obj = Instantiate(poolObjectSet.ObjectToPool);
                    obj.SetActive(false);
                    poolObjectSet.PooledObjects.Add(obj);
                }

                poolObjectSetDictionary.Add(poolObjectSet.PoolObjectType, poolObjectSet);
            }
        }
        
        public GameObject GetPooledObject(PoolObjectType pooledObjectType)
        {
            var poolObjectSet = poolObjectSetDictionary[pooledObjectType];
            var pooledObjects = poolObjectSet.PooledObjects;

            foreach (var pooledObject in pooledObjects)
            {
                if (!pooledObject.activeInHierarchy)
                {
                    return pooledObject;
                }
            }

            if (poolObjectSet.ShouldExpand)
            {
                GameObject obj = Instantiate(poolObjectSet.ObjectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);

                return obj;
            }

            return null;
        }
    }
}