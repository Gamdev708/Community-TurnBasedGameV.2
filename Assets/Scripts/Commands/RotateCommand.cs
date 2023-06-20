using System.Collections;
using System.Collections.Generic;
using TBRPG.Control;
using UnityEngine;

namespace TBRPG.Commands
{
    public class RotateCommand : ICommand
    {
        Transform charcter;
        Quaternion rotation;
        Quaternion intialrotation;

        public RotateCommand(Transform charcter, Quaternion rotation)
        {
            if (charcter == null)
            {
                Debug.LogError("Character transform is null!");
            }
            this.charcter = charcter;
            this.rotation = rotation;
            intialrotation = charcter.rotation;
        }

        public void Execute()
        {
            Quaternion newRotation = Quaternion.Inverse(rotation);
            charcter.rotation = newRotation;
        }

        public void Undo()
        {
            charcter.rotation = intialrotation;
        }
    }
}