using System.Collections;
using System.Collections.Generic;
using TBRPG.Control;
using UnityEngine;

namespace TBRPG.StateMachine
{
    public interface IGameState
    {
        /// <summary>
        /// This Method is when start
        /// </summary>
        /// <param name="controller"></param>
        void HandleInput(GameController controller);
        void Update(GameController controller);
        void Exit(GameController controller);
        void Enter(GameController controller);
    }
}