using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Core
{
    enum BattleStates
    {
        START, PLAYERACTION,PLAYERMOVE,ENEMYMOVE,BUSY, WON, LOST
    }
    enum BattleStyle
    {
        SOLO, TEAM
    }

    public class BattleSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}