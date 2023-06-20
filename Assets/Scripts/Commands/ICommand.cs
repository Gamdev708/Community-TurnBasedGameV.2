using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBRPG.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}