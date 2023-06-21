using System;
using TBRPG.Commands;
using TBRPG.Control;
using UnityEngine;

namespace TBRPG.StateMachine
{
    public class PlayerTurnState : IGameState
    {
        //Uniquly Identify the Time for each Skill command
        private Transform playerCharacter;
        Vector3 InitialLocation;
        Quaternion intialrotation;
        float speed;
        CommandTypes commandTypes;
        bool hasCompletedAction = false;

        public PlayerTurnState()
        {

        }

        public PlayerTurnState(Transform playerCharacter, Vector3 initialLocation, Quaternion intialrotation, float speed)
        {
            this.playerCharacter = playerCharacter;
            InitialLocation = initialLocation;
            this.intialrotation = intialrotation;
            this.speed = speed;
            hasCompletedAction = false;
        }

        public void Enter(GameController controller)
        {
            controller.ChangeState(this);
        }

        public void Exit(GameController controller)
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            playerCharacter = null;
            InitialLocation = Vector3.zero;
            intialrotation = Quaternion.identity;
            speed = 0;
            hasCompletedAction = false;
        }

        public void HandleInput(GameController controller)
        {
            if (!controller.IsInputAllowed()) { return; }

            //Uncomment this code for the dEvlopment when UI is available
            //if(controller.GetTarget()==null && controller.GetCommandIndex() == 0) { return; }

            //switch (commandTypes)
            //{
            //    case CommandTypes.None:
            //        break;
            //    case CommandTypes.Attack:
            //        MeleeAttack(controller);
            //        break;
            //    case CommandTypes.Heal:
            //        Heal(controller);
            //        break;
            //    case CommandTypes.Withdraw:
            //        Withdraw(controller);
            //        break;
            //    default:
            //        break;
            //}

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MeleeAttack(controller);
            }

        }
      
        public void Update(GameController controller)
        {
            //if (controller.GetTurnQueue().Count == 0)
            //{
            //    controller.SetTurnQueue();
            //}
            if (controller.IsTurnOver())
            {
                hasCompletedAction=true;
                controller.StartNextTurn();
            }

        }



        private void Withdraw(GameController controller)
        {
            throw new NotImplementedException();
        }

        private void Heal(GameController controller)
        {
            throw new NotImplementedException();
        }

        private void MeleeAttack(GameController controller)
        {
            controller.AddCommands(new AttackCommand(playerCharacter, controller.GetTarget()));
            controller.AddCommands(new MoveCommand(playerCharacter, InitialLocation, speed));
            controller.AddCommands(new RotateCommand(playerCharacter, intialrotation));
            controller.ExecuteCommandListWithDelay();
        }

    }
}