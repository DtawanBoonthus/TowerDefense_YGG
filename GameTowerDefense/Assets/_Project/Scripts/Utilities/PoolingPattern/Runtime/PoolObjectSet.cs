using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Utilities.PoolingPattern.Runtime
{
    [Serializable]
    public sealed class PoolObjectSet
    {
        public PoolObjectType PoolObjectType;
        public int AmountToPool;
        public GameObject ObjectToPool;
        public bool ShouldExpand; 
        [HideInInspector] public List<GameObject> PooledObjects;
    }
}