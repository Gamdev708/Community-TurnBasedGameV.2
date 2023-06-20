using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Abilities
{
    public abstract class FilterStrategy : ScriptableObject
    {
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);
    }
}