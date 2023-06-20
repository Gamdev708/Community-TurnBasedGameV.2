using System;
using System.Collections.Generic;
using UnityEngine;


namespace TBG.MAP
{

    [CreateAssetMenu(fileName = "Regions", menuName = "Map/Regions", order = 0)]
    public class Regions : ScriptableObject
    {
        [SerializeField] public string name;
        [SerializeField] public int RegID;
        [SerializeField] public int minLevel;
        [SerializeField] public int maxLevel;
        [SerializeField] public List <Missions> missions = new List<Missions>();
    }
}