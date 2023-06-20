using System.Collections;
using System.Collections.Generic;
using TBRPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace TBRPG.Commands
{
    public class MoveCommand : ICommand
    {
        private Mover Mover;
        private Vector3 movetoLocation;
        private Vector3 previousLocation;
        private float MoveSpeed;

        public MoveCommand(Transform character, Vector3 movetoLocation, float moveSpeed)
        {
            Mover = character.GetComponent<Mover>();
            this.movetoLocation = movetoLocation;
            previousLocation = character.position;
            MoveSpeed = moveSpeed;
        }


        public void Execute()
        {
            Mover.StartMoveAction(movetoLocation, MoveSpeed);
        }

        public void Undo()
        {
            Mover.StartMoveAction(previousLocation, MoveSpeed);
        }
    }
}