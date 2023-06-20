using System;
using System.Collections.Generic;
using UnityEngine;


namespace TBG.MAP
{

    [CreateAssetMenu(fileName = "Objective", menuName = "Map/Missions/Objective", order = 0)]
    public class ObjectivePlayer : ScriptableObject
    {
        [SerializeField] public int PointsNeed;
    }
}