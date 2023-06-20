using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(AbilityData data, Action finished);
    }

}