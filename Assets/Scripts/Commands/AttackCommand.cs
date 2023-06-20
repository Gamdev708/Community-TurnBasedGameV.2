using System.Collections;
using System.Collections.Generic;
using TBRPG.Combat;
using TBRPG.Movement;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace TBRPG.Commands
{
    public class AttackCommand : ICommand
    {
        private Fighter Fighter;
        private Mover Mover;
        private GameObject Target;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        public float waittime;

        public AttackCommand(Transform character, GameObject target)
        {
            Fighter = character.GetComponent<Fighter>();
            Mover = character.GetComponent<Mover>();
            Target = target;
            _originalPosition = character.position;
            _originalRotation = character.rotation;
        }

        public void Execute()
        {
            Fighter.Attack(Target);
        }

        public void Undo()
        {
            Mover.MoveTo(_originalPosition, 10);
        }


    }
}