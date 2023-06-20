using System;
using System.Collections.Generic;
using UnityEngine;


namespace TBG.MAP
{

    [CreateAssetMenu(fileName = "Missions", menuName = "Map/Missions/Mission", order = 0)]
    public class Missions : ScriptableObject
    {
        [SerializeField] public string name;
        [SerializeField] public string reward;
        [SerializeField] public int sceneINDEX;
        [SerializeField] public int MissionID;
        [SerializeField] public ObjectivePlayer objective;
        [SerializeField] public MissionState missionState;

    }

    public enum MissionState
    {
        Locked,
        Available,
        Finished
    }
}